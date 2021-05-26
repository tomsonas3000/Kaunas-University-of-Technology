package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"math/rand"
	"os"
	"sync"
	"time"
)

var x int
var isDone bool

func main() {
	var students []Student
	var wg sync.WaitGroup
	var dataMonitor = initializeMonitor(10)
	var resultsMonitor = initializeMonitor(25)

	jsonFile, err := os.Open("IFF-8-11.JorudasT_L1_dat_1.json")
	if err != nil {
		log.Fatal(err)
	}

	studentsJSON, err := ioutil.ReadAll(jsonFile)
	if err != nil {
		log.Fatal(err)
	}
	//nolint
	json.Unmarshal([]byte(studentsJSON), &students)
	// fmt.Println("------Before------")
	// for _, student := range students {
	// 	fmt.Println(student)
	// }
	wg.Add(1)
	for i := 0; i < 5; i++ {
		wg.Add(1)
		go WorkWithMonitor(&dataMonitor, students, &resultsMonitor, &wg)
	}
	go AddToMonitor(&dataMonitor, students, &wg)
	wg.Wait()
	fmt.Println("-----After------")
	for _, student := range resultsMonitor.Students {
		fmt.Println(student)
	}
}

func AddToMonitor(monitor *Monitor, students []Student, wg *sync.WaitGroup) {
	defer wg.Done()
	for i, student := range students {
		fmt.Printf("Pridedam %v\n", i)
		if i+1 == 19 {
			fmt.Println(student)
			isDone = true
			fmt.Println(isDone)
			monitor.Insert(student, true)
		}
		monitor.Insert(student, false)
	}
}

func WorkWithMonitor(monitor *Monitor, students []Student, resultsMonitor *Monitor, wg *sync.WaitGroup) {
	defer wg.Done()
	for i := 0; i < len(students); i++ {
		if isDone && monitor.CurrentSize == 0 {
			return
		}
		fmt.Printf("DONE %v MONITOR CURRENT SIZE NAXUI %v", monitor.NuDone, monitor.CurrentSize)
		var student = monitor.Remove(wg)
		AddToResultsMonitor(student)
		x++
		fmt.Printf("Isimam %v %v\n", x, student)
		resultsMonitor.Insert(student, false)
	}
}

func AddToResultsMonitor(student Student) bool {
	rand.Seed(time.Now().UnixNano())
	n := rand.Intn(3)
	time.Sleep(time.Duration(n) * time.Second)
	return true
}

type Student struct {
	Name  string
	Year  int
	Grade float64
	Hash  string
}

type Monitor struct {
	Students    []Student
	Mutex       *sync.Mutex
	Cond        *sync.Cond
	CurrentSize int
	Counter     int
	From        int
	To          int
	NuDone      bool
}

func initializeMonitor(monitorSize int) Monitor {
	var studentsArray = make([]Student, monitorSize)
	var mutex = sync.Mutex{}
	var cond = sync.NewCond(&mutex)
	var monitor = Monitor{Students: studentsArray, Mutex: &mutex, Cond: cond, Counter: 0}
	return monitor
}

func (monitor *Monitor) Insert(student Student, done bool) {
	monitor.Mutex.Lock()
	for monitor.CurrentSize == len(monitor.Students) {
		monitor.Cond.Wait()
	}
	monitor.Students[monitor.To] = student
	monitor.To = (monitor.To + 1) % len(monitor.Students)
	monitor.CurrentSize++
	monitor.NuDone = true
	monitor.Cond.Broadcast()
	monitor.Mutex.Unlock()
}

func (monitor *Monitor) Remove(wg *sync.WaitGroup) Student {
	monitor.Mutex.Lock()
	for monitor.CurrentSize == 0 {
		monitor.Cond.Wait()
	}
	var student = monitor.Students[monitor.From]
	var emptyStudent Student
	monitor.Students[monitor.From] = emptyStudent
	monitor.From = (monitor.From + 1) % len(monitor.Students)
	monitor.CurrentSize--
	monitor.Cond.Broadcast()
	monitor.Mutex.Unlock()
	return student
}

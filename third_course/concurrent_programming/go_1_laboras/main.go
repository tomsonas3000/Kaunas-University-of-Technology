package main

import (
	"encoding/json"
	"errors"
	"fmt"
	"io/ioutil"
	"log"
	"os"
	"sync"
)

func main() {
	var wg sync.WaitGroup
	var dataMonitor = InitializeMonitor(10)
	var resultsMonitor = InitializeMonitor(35)
	var players = ReadFromFile("IFF-8-11_JorudasT_L1_dat_2.json")
	wg.Add(1)
	for i := 0; i < 5; i++ {
		wg.Add(1)
		go WorkWithMonitor(&dataMonitor, &resultsMonitor, &wg)
	}
	go AddToMonitor(&dataMonitor, players, &wg)
	wg.Wait()
	WriteToFile(players, resultsMonitor.Players)
}

func ReadFromFile(fileName string) []Player {
	var players []Player
	jsonFile, err := os.Open(fileName)
	if err != nil {
		log.Fatal(err)
	}

	playersJSON, err := ioutil.ReadAll(jsonFile)
	if err != nil {
		log.Fatal(err)
	}
	//nolint
	json.Unmarshal([]byte(playersJSON), &players)

	err = jsonFile.Close()
	if err != nil {
		log.Fatal(err)
	}

	return players

}

func AddToMonitor(monitor *Monitor, players []Player, wg *sync.WaitGroup) {
	defer wg.Done()
	for _, player := range players {
		monitor.Insert(player)
	}
	monitor.FinishInsert()
}

func WorkWithMonitor(monitor *Monitor, resultsMonitor *Monitor, wg *sync.WaitGroup) {
	defer wg.Done()
	for {
		var player, err = monitor.Remove()
		if err != nil {
			break
		}
		resultsMonitor.InsertToResults(player)

	}
}

func InitializeMonitor(monitorSize int) Monitor {
	var playersArray = make([]Player, monitorSize)
	var mutex = sync.Mutex{}
	var cond = sync.NewCond(&mutex)
	var monitor = Monitor{Players: playersArray, Mutex: &mutex, Cond: cond, Counter: 0, IsDone: false}
	return monitor
}

func WriteToFile(initialData []Player, resultsData []Player) {
	file, err := os.Create("IFF-8-11_JorudasT_L1_rez.txt")
	if err != nil {
		log.Fatal(err)
		return
	}
	stringLine := fmt.Sprintln("----------------------------------------------------------")
	header := fmt.Sprintf("|%-10s|%-11s|%-17s|%-15s|\n", "Name", "Shots taken", "Points per game", "Points per shot")
	writtenBytes0, err := file.WriteString("Initial data:\n")
	if err != nil {
		fmt.Println(writtenBytes0)
		log.Fatal(err)
	}
	writtenBytes1, err := file.WriteString(header)
	if err != nil {
		fmt.Println(writtenBytes1)
		log.Fatal(err)
	}
	writtenBytes2, err := file.WriteString(stringLine)
	if err != nil {
		fmt.Println(writtenBytes2)
		log.Fatal(err)
	}
	for _, player := range initialData {
		s := fmt.Sprintf("|%-10s|%11d|%17.2f|%15.2f|\n", player.Name, player.ShotsTaken, player.Ppg, player.PointsPerShot)
		writtenBytes, err := file.WriteString(s)
		if err != nil {
			fmt.Println(writtenBytes)
			log.Fatal(err)
		}
	}
	writtenBytes3, err := file.WriteString(stringLine)
	if err != nil {
		fmt.Println(writtenBytes3)
		log.Fatal(err)
	}
	writtenBytes4, err := file.WriteString("Results data:\n")
	if err != nil {
		fmt.Println(writtenBytes4)
		log.Fatal(err)
	}
	writtenBytes5, err := file.WriteString(stringLine)
	if err != nil {
		fmt.Println(writtenBytes5)
		log.Fatal(err)
	}
	for _, player := range resultsData {
		if player.PointsPerShot <= 0 {
			continue
		}
		s := fmt.Sprintf("|%-10s|%11d|%17.2f|%15.2f|\n", player.Name, player.ShotsTaken, player.Ppg, player.PointsPerShot)
		writtenBytes, err := file.WriteString(s)
		if err != nil {
			fmt.Println(writtenBytes)
			log.Fatal(err)
		}
	}
	writtenBytes6, err := file.WriteString(stringLine)
	if err != nil {
		fmt.Println(writtenBytes6)
		log.Fatal(err)
	}
	err = file.Close()
	if err != nil {
		fmt.Println(err)
		return
	}
}

type Player struct {
	Name          string
	ShotsTaken    int
	Ppg           float64
	PointsPerShot float64
}

type Monitor struct {
	Players     []Player
	Mutex       *sync.Mutex
	Cond        *sync.Cond
	CurrentSize int
	Counter     int
	From        int
	To          int
	IsDone      bool
}

func (monitor *Monitor) Insert(player Player) {
	monitor.Mutex.Lock()
	for monitor.CurrentSize == len(monitor.Players) {
		monitor.Cond.Wait()
	}
	monitor.Players[monitor.To] = player
	monitor.To = (monitor.To + 1) % len(monitor.Players)
	monitor.CurrentSize++
	monitor.Cond.Broadcast()
	monitor.Mutex.Unlock()
}

func (monitor *Monitor) InsertToResults(player Player) {
	monitor.Mutex.Lock()
	var pps = player.Ppg / float64(player.ShotsTaken)
	player.PointsPerShot = pps
	if player.PointsPerShot > 5 {
		var i = len(monitor.Players) - 1
		for player.PointsPerShot > monitor.Players[i].PointsPerShot {
			i--
			if i < 0 {
				break
			}
			monitor.Players[i+1] = monitor.Players[i]
		}
		monitor.Players[i+1] = player
		monitor.CurrentSize++
	}
	monitor.Cond.Broadcast()
	monitor.Mutex.Unlock()
}

func (monitor *Monitor) Remove() (Player, error) {
	defer monitor.Mutex.Unlock()
	monitor.Mutex.Lock()
	for monitor.CurrentSize == 0 {
		if monitor.IsDone {
			var emptyPlayer Player
			return emptyPlayer, errors.New("No player")
		}
		monitor.Cond.Wait()
	}
	var player = monitor.Players[monitor.From]
	var emptyPlayer Player
	monitor.Players[monitor.From] = emptyPlayer
	monitor.From = (monitor.From + 1) % len(monitor.Players)
	monitor.CurrentSize--
	monitor.Cond.Broadcast()
	return player, nil
}

func (monitor *Monitor) FinishInsert() {
	monitor.Mutex.Lock()
	monitor.IsDone = true
	monitor.Cond.Broadcast()
	monitor.Mutex.Unlock()
}

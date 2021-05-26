package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"os"
)

func main() {
	var players = ReadFromFile("IFF-8-11_JorudasT_L2_dat_2.json")
	var dataChannel = make(chan Player)
	var workChannel = make(chan Player)
	var workMsgChannel = make(chan bool)
	var syncChannel = make(chan int)
	var computeChannel = make(chan Player)
	var resultsChannel = make(chan [40]Player)
	workerThreadCount := 3
	go func() {
		syncValue := 0
		for {
			value := <-syncChannel
			syncValue = syncValue + value
			if syncValue >= workerThreadCount {
				close(workMsgChannel)
				close(computeChannel)
			}
		}
	}()
	go GetData(dataChannel, workChannel, workMsgChannel)
	go SendData(computeChannel, resultsChannel)
	for i := 0; i < workerThreadCount; i++ {
		go WorkWithData(workChannel, computeChannel, workMsgChannel, syncChannel)
	}
	for _, player := range players {
		dataChannel <- player
	}
	close(dataChannel)
	playersResults := <-resultsChannel
	WriteToFile(players, playersResults)
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
func WriteToFile(initialData []Player, resultsData [40]Player) {
	file, err := os.Create("IFF-8-11_JorudasT_L2_rez.txt")
	if err != nil {
		log.Fatal(err)
		return
	}
	stringLine := fmt.Sprintln("------------------------------------------------------------")
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
	for i, player := range initialData {
		s := fmt.Sprintf("|%-2d|%-10s|%11d|%17.2f|%15.2f|\n", i+1, player.Name, player.ShotsTaken, player.Ppg, player.PointsPerShot)
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
	for i, player := range resultsData {
		if player.PointsPerShot <= 0 {
			continue
		}
		s := fmt.Sprintf("|%-2d|%-10s|%11d|%17.2f|%15.2f|\n", i+1, player.Name, player.ShotsTaken, player.Ppg, player.PointsPerShot)
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

func GetData(dataChannel <-chan Player, workChannel chan<- Player, workMsgChannel <-chan bool) {
	var dataArray [10]Player
	var to = 0
	var from = 0
	var size = 0
	for {
		if size < len(dataArray) {
			player := <-dataChannel
			dataArray[to] = player
			size++
			to = (to + 1) % len(dataArray)

		}
		if size > 0 {
			<-workMsgChannel
			workChannel <- dataArray[from]
			var blankPlayer Player
			dataArray[from] = blankPlayer
			from = (from + 1) % len(dataArray)
			size--
			player, check := <-dataChannel
			if !check {
				for msg := range workMsgChannel {
					if msg {
						workChannel <- dataArray[from]
						var blankPlayer Player
						dataArray[from] = blankPlayer
						from = (from + 1) % len(dataArray)
					}
				}
				close(workChannel)
				break
			} else {
				dataArray[to] = player
				size++
				to = (to + 1) % len(dataArray)
			}
		}
	}
}

func WorkWithData(workChannel <-chan Player, computeChannel chan<- Player, workMsgChannel chan<- bool, syncChannel chan<- int) {
	defer func() { syncChannel <- 1 }()
	for {
		workMsgChannel <- true
		receivedPlayer := <-workChannel
		if receivedPlayer == (Player{}) {
			break
		}
		var pps = receivedPlayer.Ppg / float64(receivedPlayer.ShotsTaken)
		receivedPlayer.PointsPerShot = pps
		if receivedPlayer.PointsPerShot > 5 {
			computeChannel <- receivedPlayer
		}
	}
}

func SendData(computeChannel chan Player, resultsChannel chan [40]Player) {
	var resultsArray [40]Player
	for player := range computeChannel {
		var i = len(resultsArray) - 1
		for player.PointsPerShot > resultsArray[i].PointsPerShot {
			i--
			if i < 0 {
				break
			}
			resultsArray[i+1] = resultsArray[i]
		}
		resultsArray[i+1] = player
	}
	resultsChannel <- resultsArray
	close(resultsChannel)
}

type Player struct {
	Name          string
	ShotsTaken    int
	Ppg           float64
	PointsPerShot float64
}

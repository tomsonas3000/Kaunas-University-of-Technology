#include <iostream>
#include <omp.h>
#include <fstream>
#include <nlohmann/json.hpp>
#include <stdio.h>
#include <cstdio>
#include <iostream>

using namespace std;
using json = nlohmann::json;


struct Player {
	string Name = "";
	int ShotsTaken = 0;
	double Ppg = 0.0;
	double PointsPerShot = 0.0;
};

struct DataMonitor {
	Player Players[10];
	omp_lock_t* Lock;
	bool IsDone = false;
	int CurrentSize = 0;
	int To = 0;
	int From = 0;
	bool Insert(Player player) {
		omp_set_lock(Lock);
		if (CurrentSize < 10) {
			Players[To] = player;
			To = (To + 1) % 10;
			CurrentSize++;
			omp_unset_lock(Lock);
			return true;
		}
		else {
			omp_unset_lock(Lock);
			return false;
		}
	}
	bool Remove(Player* player) {
		omp_set_lock(Lock);
		if (CurrentSize > 0) {
			*player = Players[From];
			Player emptyPlayer = Player();
			Players[From] = emptyPlayer;
			From = (From + 1) % 10;
			CurrentSize--;
			omp_unset_lock(Lock);
			return true;
		}
		else {
			if (IsDone && CurrentSize == 0)
			{
				omp_unset_lock(Lock);
				return true;

			}
			omp_unset_lock(Lock);
			return false;
		}
	}
};

struct ResultsMonitor {
	Player Players[35];
	omp_lock_t* Lock;
	bool IsDone = false;
	int CurrentSize = 0;
	int To = 0;
	int From = 0;
	void Insert(Player player) {
		omp_set_lock(Lock);
		double pps = player.Ppg / double(player.ShotsTaken);
		player.PointsPerShot = pps;
		if (player.PointsPerShot > 5) {
			int i = 34;
			while (player.PointsPerShot > Players[i].PointsPerShot) {
				i--;
				if (i < 0) {
					break;
				}
				Players[i + 1] = Players[i];
			}
			Players[i + 1] = player;
			CurrentSize++;
		}
		omp_unset_lock(Lock);
	}
};

vector<Player> JsonToVector(json json);
int WriteToFile(vector<Player> initialData, Player players[35]);

int main() {
	ifstream i("../IFF-8-11_JorudasT_L1_dat_3.json");
	json j;
	i >> j;
	vector<Player> initialPlayerData = JsonToVector(j);
	omp_lock_t * lockData = new omp_lock_t;
	omp_lock_t * lockResults = new omp_lock_t;
	omp_init_lock(lockData);
	omp_init_lock(lockResults);
	DataMonitor dataMonitor = DataMonitor();
	ResultsMonitor resultsMonitor = ResultsMonitor();
	dataMonitor.Lock = lockData;
	resultsMonitor.Lock = lockResults;

#pragma omp parallel num_threads(5)
	{
		int thread_id = omp_get_thread_num();
		if (thread_id == 0) {
			for (int i = 0; i < initialPlayerData.size(); i++) {
				while (!dataMonitor.Insert(initialPlayerData[i]))
					cout << "**Pridejimas**Kritine sekcija uzimta, bandom dar karta" << endl;
			}
			dataMonitor.IsDone = true;
		}
		else {
			while (!dataMonitor.IsDone || dataMonitor.CurrentSize != 0)
			{
				Player player = Player();
				while (!dataMonitor.Remove(&player))
					cout << "**Isemimas** Kritine sekcija uzimta, bandom dar karta" << endl;
				if (player.Name == "")
					break;
				resultsMonitor.Insert(player);
			
			}
			
		}
	}
	omp_destroy_lock(dataMonitor.Lock);
	omp_destroy_lock(resultsMonitor.Lock);
	if (!WriteToFile(initialPlayerData, resultsMonitor.Players) == 0) {
		cout << "Writing to file failed" << endl;
	}
}

vector<Player> JsonToVector(json json) {
	vector<Player> players(json.size());
	for (int i = 0; i < (int)json.size(); i++) {
		Player player = Player();
		player.Name = json[i].at("name");
		player.ShotsTaken = json[i].at("shotsTaken");
		player.Ppg = json[i].at("ppg");
		players[i] = player;
	}
	return players;
}

int WriteToFile(vector<Player> initialData, Player players[35]) {
	ofstream file;
	char buffer[100];
	int returnValue;
	file.open("../IFF-8-11_JorudasT_L1_rez.txt");
	file << "Initial data:" << endl;
	returnValue = sprintf_s(buffer, "|%-10s|%-11s|%-17s|%-15s|\n", "Name", "Shots taken", "Points per game", "Points per shot");
	file << buffer;
	file << "----------------------------------------------------------" << endl;
	for (Player player : initialData) {
		int n = player.Name.length();
		char char_array[10];
		strcpy_s(char_array, player.Name.c_str());

		returnValue = sprintf_s(buffer, "|%-10s|%11d|%17.2f|%15.2f|\n", char_array, player.ShotsTaken, player.Ppg, player.PointsPerShot);
		file << buffer;
	}
	file << "----------------------------------------------------------" << endl;
	file << "Results data:" << endl;
	file << "----------------------------------------------------------" << endl;
	for (int i = 0; i < 35; i++) {
		if (players[i].PointsPerShot <= 0) {
			continue;
		}
		int n = players[i].Name.length();
		char char_array[10];
		strcpy_s(char_array, players[i].Name.c_str());
		returnValue = sprintf_s(buffer, "|%-10s|%11d|%17.2f|%15.2f|\n", char_array, players[i].ShotsTaken, players[i].Ppg, players[i].PointsPerShot);
		file << buffer;
	}
	file << "----------------------------------------------------------" << endl;
	file.close();
	return 0;
}



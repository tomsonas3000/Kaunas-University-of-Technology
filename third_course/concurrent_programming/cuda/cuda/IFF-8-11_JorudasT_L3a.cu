
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <sstream>

const int ARRAY_SIZE = 33;
const int NAME_SIZE = 15;
const int ARRAY_SIZE_RESULTS = ARRAY_SIZE * NAME_SIZE;


using namespace std;

struct Player {
	char Name[NAME_SIZE];
	int ShotsTaken = 0;
	double Ppg = 0.0;
};

__global__ void workWithData(Player* players, int* index, char* results);
__device__ char* my_strcpy(char *dest, const char *src, char pps);
__device__ int filterData(Player player, char* filteredPlayer);
vector<string> split(const string &s, char delim);

int main()
{
	string line;
	ifstream file("IFF-8-11_JorudasT_L3_2.txt");
	Player players[ARRAY_SIZE];
	if (file.is_open())
	{
		int i = 0;
		while (getline(file, line))
		{
			vector<string> s = split(line, ',');
			Player player = Player();
			strcpy(player.Name, s[0].c_str());
			player.ShotsTaken = stoi(s[1]);
			player.Ppg = stod(s[2]);
			players[i] = player;
			i++;
		}
		file.close();
	}
	else cout << "Unable to open file";

	char results[ARRAY_SIZE_RESULTS];
	int initialIndex = 0;

	int *device_index;
	Player *device_players;
	char *device_results;

	cudaMalloc(&device_players, ARRAY_SIZE * sizeof(Player));
	cudaMalloc(&device_results, ARRAY_SIZE_RESULTS * sizeof(char));
	cudaMalloc(&device_index, sizeof(int));

	cudaMemcpy(device_players, &players, ARRAY_SIZE * sizeof(Player), cudaMemcpyHostToDevice);
	cudaMemcpy(device_results, &results, ARRAY_SIZE_RESULTS * sizeof(char), cudaMemcpyHostToDevice);
	cudaMemcpy(device_index, &initialIndex, sizeof(int), cudaMemcpyHostToDevice);

	workWithData << <1,5 >> > (device_players, device_index, device_results);
	cudaDeviceSynchronize();
	cudaMemcpy(&initialIndex, device_index, sizeof(int), cudaMemcpyDeviceToHost);
	cudaMemcpy(&results, device_results, ARRAY_SIZE_RESULTS * sizeof(char), cudaMemcpyDeviceToHost);

	ofstream resultsFile;
	resultsFile.open("IFF-8-11_JorudasT_L3a_rez.txt");

	for (int i = 0; i < initialIndex; i++) {
		resultsFile << results[i];
	}
	file.close();
	cout << endl << "Isspausdinta " << initialIndex / NAME_SIZE << " elementai" << endl;

	cudaFree(device_players);
	cudaFree(device_results);
	cudaFree(device_index);

    return 0;
}

vector<string> split(const string &s, char delim) {
	vector<string> result;
	stringstream ss(s);
	string item;

	while (getline(ss, item, delim)) {
		result.push_back(item);
	}

	return result;
}


__global__ void workWithData(Player *players, int *index, char *results) {
	int lengthOfArrayPart = ARRAY_SIZE / blockDim.x;
	int startIndex = lengthOfArrayPart * threadIdx.x;
	int endIndex;
	if (threadIdx.x == blockDim.x - 1) {
		endIndex = ARRAY_SIZE;
	}
	else {
		endIndex = lengthOfArrayPart * (threadIdx.x + 1);
	}
	for (int i = startIndex; i < endIndex; i++) {
		Player player = players[i];
		char filteredPlayer[NAME_SIZE];
		int check = filterData(player, filteredPlayer);
		if (check == 0) {
			int localIndex = atomicAdd(index, NAME_SIZE);
			for (int j = 0; j < NAME_SIZE; j++) {
				results[localIndex + j] = filteredPlayer[j];
			}
		}
		
	}
}

__device__ char* my_strcpy(char *dest, const char *src, char pps) {
	int i = 0;
	do {
		dest[i] = src[i];
	} while (src[i++] != 0);
	i--;
	dest[i++] = '-';
	dest[i++] = pps;
	for (int j = i; j < NAME_SIZE; j++) {
		dest[j] = ' ';
	}
	return dest;
}

__device__ int filterData(Player player, char* filteredPlayer) {
	int pps = player.Ppg / player.ShotsTaken;
	if (pps < 5) {
		return 1;
	}
	char ppsChar = '0' + pps;
	my_strcpy(filteredPlayer, player.Name, ppsChar);
	return 0;
}
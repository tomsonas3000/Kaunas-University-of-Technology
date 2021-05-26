import matplotlib.pyplot as plt
import numpy as np
from sklearn.linear_model import LinearRegression


def read_file():
    dataMatrix = [[], []]
    f = open("sunspot.txt", "r")
    lines = f.readlines()

    for line in lines:
        splitLine = line.split('\t')
        dataMatrix[0].append(int(splitLine[0]))
        dataMatrix[1].append(int(splitLine[1]))
    f.close()
    return dataMatrix


def plot_2d(dataMatrix, title, xlabel, ylabel):
    plt.title(title)
    plt.xlabel(xlabel)
    plt.ylabel(ylabel)
    plt.plot(dataMatrix[0], dataMatrix[1])
    plt.show()


def plot_2d_prediction(x, y, title, xlabel, ylabel):
    plt.title(title)
    plt.xlabel(xlabel)
    plt.ylabel(ylabel)
    plt.plot(x, y[0], 'red', label="Tikra")
    plt.plot(x, y[1], 'blue', label="Prognozuota")
    plt.legend()
    plt.show()


def create_input_output_data(dataMatrix, n):
    P = []
    T = []
    sunspots = dataMatrix[1]
    for i in range(len(sunspots) - n):
        entry = []
        indexT = 0
        for j in range(i, i + n):
            entry.append(sunspots[j])
            indexT = j + 1
        P.append(entry)
        T.append(sunspots[indexT])
    return P, T


def plot_3d(P, T):
    fig = plt.figure()
    ax = plt.axes(projection="3d")
    ax.set_title("Įvesties ir išvesties santykis")
    firstValuesP = []
    secondValuesP = []
    for i in range(len(P)):
        firstValuesP.append(P[i][0])
        secondValuesP.append(P[i][1])
    ax.scatter3D(firstValuesP, T, secondValuesP)
    plt.show()


def plot_hist(vector, xlabel, ylabel, title):
    plt.hist(vector)
    plt.xlabel(xlabel)
    plt.ylabel(ylabel)
    plt.title(title)


data = read_file()
plot_2d(data, "Saulės dėmių aktyvumas 1700-2014", "Metai", "Dėmių aktyvumas")

inputP, outputT = create_input_output_data(data, 10)  # n

plot_3d(inputP, outputT)

PuTrain = np.array(inputP[:200])
TuTrain = np.array(outputT[:200])

linearModel = LinearRegression().fit(PuTrain, TuTrain)
print("Gauti koeficientai", linearModel.coef_)

TsuTrain = linearModel.predict(PuTrain)
plot_2d_prediction(data[0][:200], [TuTrain, TsuTrain], 'Mokymosi duomenų prognozė', 'Metai', "Saulės dėmių aktyvumas")

PuTest = np.array(inputP[200:])
TuTest = np.array(outputT[200:])
TsuTest = linearModel.predict(PuTest)

lenTo = 200 + len(PuTest)

plot_2d_prediction(data[0][200:lenTo], [TuTest, TsuTest], 'Testinių duomenų prognozė', 'Metai',
                   "Saulės dėmių aktyvumas")

testError = TuTest - TsuTest
plot_2d([data[0][200:lenTo], testError], "Prognozės klaida", "Metai", "Klaidos dydis")

plot_hist(testError, "Klaidos dydis", "Dažnis", "Prognozės klaidos histograma")
plt.show()
MSE = (1 / len(testError)) * sum(map(lambda x: x * x, testError))
print("MSE klaidos reikšmė", MSE)

MAD = np.median(list(map(lambda x: abs(x), testError)))
print("MAD klaidos reikšmė", MAD)

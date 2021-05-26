import matplotlib.pyplot as plt
import numpy as np
from sklearn.linear_model import LinearRegression


class AdaptiveLinearNeuron(object):
    def __init__(self, rate=0.01, niter=10):
        self.rate = rate
        self.niter = niter

    def fit(self, X, y):
        """Fit training data
        X : Training vectors, X.shape : [#samples, #features]
        y : Target values, y.shape : [#samples]
        """

        # weights
        self.weight = np.zeros(1 + X.shape[1])

        # Number of misclassifications
        self.errors = []

        # Cost function
        self.cost = []

        for i in range(self.niter):
            output = self.net_input(X)
            errors = y - output
            self.weight[1:] += self.rate * X.T.dot(errors)
            self.weight[0] += self.rate * errors.sum()
            cost = (errors ** 2).sum() / 2.0
            self.cost.append(cost)
        return self

    def net_input(self, X):
        """Calculate net input"""
        return np.dot(X, self.weight[1:]) + self.weight[0]

    def activation(self, X):
        """Compute linear activation"""
        return self.net_input(X)

    def predict(self, X):
        """Return class label after unit step"""
        return np.where(self.activation(X) >= 0.0, 1, -1)


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


data = read_file()
# print(data)

inputP, outputT = create_input_output_data(data, 2)

PuTrain = np.array(inputP[:200])
TuTrain = np.array(outputT[:200])

# learning rate = 0.01
aln1 = AdaptiveLinearNeuron(0.00001, 10).fit(PuTrain, TuTrain)

TsuTrain = aln1.predict(PuTrain)

#plot_2d_prediction(data[0][:200], [TuTrain, TsuTrain], 'Mokymosi duomenų prognozė', 'Metai', "Saulės dėmių aktyvumas")

PuTest = np.array(inputP[200:])
TuTest = np.array(outputT[200:])
TsuTest = aln1.predict(PuTest)

testError = TuTest - TsuTest
lenTo = 200 + len(PuTest)
MSE = (1 / lenTo) * sum(map(lambda x: x * x, testError))
print(MSE, "NU")


plt.plot(range(1, len(aln1.cost) + 1), aln1.cost, marker='o')
plt.xlabel('Epochs')
plt.ylabel('Sum-squared-error')
plt.title('Adaptive Linear Neuron - Learning rate 0.000001')

plt.show()

print("NU")

# TsuTrain = linearModel.predict(PuTrain)
# plot_2d_prediction(data[0][:200], [TuTrain, TsuTrain], 'Mokymosi duomenų prognozė', 'Metai', "Saulės dėmių aktyvumas")

# PuTest = np.array(inputP[200:])
# print(len(PuTest))
# TuTest = np.array(outputT[200:])
# TsuTest = linearModel.predict(PuTest)

# lenTo = 200 + len(PuTest)

# plot_2d_prediction(data[0][200:lenTo], [TuTest, TsuTest], 'Testinių duomenų prognozė', 'Metai',
#                   "Saulės dėmių aktyvumas")

# testError = TuTest - TsuTest
# plot_2d([data[0][200:lenTo], testError], "Prognozės klaida", "Metai", "Klaidos dydis")

# plt.hist(testError)
# plt.show()

# MSE = (1 / lenTo) * sum(map(lambda x: x * x, testError))
# print(MSE)

# MAD = np.median(list(map(lambda x: abs(x), testError)))
# print(MAD)

# print(Tu)

# plot_2d(data, "Saulės dėmių aktyvumas 1700-2014", "Metai","Saulės dėmių aktyvumas" )
# plot_3d(inputP, outputT)

from matplotlib import pyplot as plt
import numpy as np


def f(x, a, b, c, d):
    if x < a:
        return 0
    if a <= x < b:
        return (x - a) / (b - a)
    if b <= x < c:
        return 1
    if c <= x < d:
        return (d - x) / (d - c)
    else:
        return 0


def plotFuzzy(title, x, y):
    plt.title(title)
    # plt.xlabel("x axis caption")
    # plt.ylabel("y axis caption")
    plt.plot(x[0], y[0], 'b')
    plt.plot(x[1], y[1], 'b')
    plt.plot(x[2], y[2], 'b')


def plotAggregate(xCalories, probabilities):
    xCalories[0][2] = getCrossingPoint(xCalories[0][2:4], y[0][2:4], 0)
    plt.fill_between(xCalories[0], [probabilities[0], probabilities[0], probabilities[0], 0], 0, color='r')

    xCalories[1][1] = getCrossingPoint(xCalories[1][0:2], y[1][0:2], 1)
    xCalories[1][2] = getCrossingPoint(xCalories[1][2:4], y[1][2:4], 1)
    plt.fill_between(xCalories[1], [0, probabilities[1], probabilities[1], 0], 0, color='r')

    xCalories[2][1] = getCrossingPoint(xCalories[2][0:2], y[2][0:2], 2)
    plt.fill_between(xCalories[2], [0, probabilities[2], probabilities[2], 0], 0, color='r')


def plotResultLines(centroidResult, momResult):
    plt.axvline(centroidResult, label="Centroid", color='c')
    plt.text(centroidResult + 1, 0.75, 'Centroid')
    plt.axvline(momResult, label="MOM", color='m')
    plt.text(momResult + 1, 0.25, 'MOM')


def getInputProbabilities(xArray, values):
    for i in range(len(xArray)):
        print(str(i) + " array ")
        for j in range(len(values)):
            print(f(values[i], xArray[i][j][0], xArray[i][j][1], xArray[i][j][2], xArray[i][j][3]))


def getInput(x, value):
    results = []
    for i in range(len(x)):
        results.append(f(value, x[i][0], x[i][1], x[i][2], x[i][3]))
    return results


def getProbabilities(fatResults, cholesterolResults, proteinResults):
    lowProbability = max(max(fatResults[0], cholesterolResults[0]),
                         max(cholesterolResults[0], proteinResults[1]),
                         max(cholesterolResults[1], proteinResults[0]))
    mediumProbability = max(min(fatResults[0], proteinResults[2]),
                            min(fatResults[2], cholesterolResults[0]),
                            min(fatResults[0], cholesterolResults[1], proteinResults[2]),
                            max(fatResults[1], proteinResults[0]),
                            min(fatResults[1], cholesterolResults[1], proteinResults[1]),
                            )
    largeProbability = max(max(fatResults[2], cholesterolResults[2]),
                           min(cholesterolResults[2], proteinResults[2]),
                           min(fatResults[2], cholesterolResults[1], proteinResults[1]),
                           min(fatResults[2], proteinResults[2]))

    probabilities = [lowProbability, mediumProbability, largeProbability]
    return probabilities


def centroidMethod(calories, probabilities):
    numerator = calories[0][1] * probabilities[0] + \
                calories[1][1] * probabilities[1] + \
                calories[1][2] * probabilities[1] + \
                calories[2][1] * probabilities[2] + \
                calories[2][2] * probabilities[2]
    denominator = (probabilities[0] + probabilities[1] + probabilities[2]) * 2

    return numerator / denominator


def meanOfMaximum(calories, probabilities):
    maximumValueIndex = probabilities.index(max(probabilities))
    return (calories[maximumValueIndex][1] + calories[maximumValueIndex][2]) / 2


def getCrossingPoint(x, y, i):
    coffs = np.polyfit(x, y, 1)
    crossingPoint = (probabilities[i] - coffs[1]) / (coffs[0])
    return crossingPoint


y = [[1, 1, 1, 0], [0, 1, 1, 0], [0, 1, 1, 1]]
xFat = [[0, 0, 10, 25], [12, 30, 50, 60], [50, 65, 100, 100]]
xCholesterol = [[0, 0, 10, 25], [12, 30, 50, 60], [40, 55, 100, 100]]
xProtein = [[0, 0, 5, 15], [10, 20, 30, 40], [30, 50, 87, 87]]
xCalories = [[0, 0, 180, 400], [190, 350, 550, 780], [600, 900, 1500, 1500]]  # output
values = [30, 20, 10]

fatResults = getInput(xFat, values[0])
cholesterolResults = getInput(xCholesterol, values[1])
proteinResults = getInput(xProtein, values[2])
probabilities = getProbabilities(fatResults, cholesterolResults, proteinResults)
print(probabilities)
plotFuzzy("Kalorijų kiekis", xCalories, y)
centroidResult = centroidMethod(xCalories, probabilities)
meanOfMaximumResult = meanOfMaximum(xCalories, probabilities)
plotAggregate(xCalories, probabilities)
plotResultLines(centroidResult, meanOfMaximumResult)

plt.show()

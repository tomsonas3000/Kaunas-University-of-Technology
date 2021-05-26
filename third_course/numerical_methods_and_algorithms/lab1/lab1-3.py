import matplotlib.pyplot as plt
import numpy as np
import sympy as sp
from scipy.misc import derivative
from prettytable import PrettyTable


def fx(x):
    return 0.48*x**5 + 1.71*x**4 - 0.67*x**3 - 4.86*x**2 - 1.33*x + 1.5


def gx(x):
    return np.exp(-x) * np.sin(x**2) + 0.001


def scanningMethod(xStart, xEnd, step, function):
    tableScan = PrettyTable()
    tableScan.field_names = ["Intervalo nr", "Iteracijos nr",
                             "Intervalo pradžia", "Intervalo pabaiga"]
    tableSimple = PrettyTable()
    tableSimple.field_names = ["Intervalo nr", "Iteracijos nr", "Saknis", "Alpha",
                               "Tikslumas"]
    tableNewton = PrettyTable()
    tableNewton.field_names = ["Intervalo nr", "Iteracijos nr",
                               "Šaknis", "Beta", "Tikslumas"]
    tableVariable = PrettyTable()
    tableVariable.field_names = ["Intervalo nr", "Iteracijos nr",
                                 "Intervalo pradžia", "Intervalo pabaiga", "Tikslumas"]
    x = xStart
    iteration = 0
    alphaFx = -20
    alphaGx = 0.1
    beta = 0.8
    intervalCount = 0
    while x < xEnd:
        if np.sign(function(x)) != np.sign(function(x+step)):
            intervalCount = intervalCount + 1
            tableScan.add_row([intervalCount, iteration, x, x + step])
            # simpleIterationMethod(alphaFx, x, function,
            #                      tableSimple, intervalCount)
            simpleIterationMethod(alphaGx, x,  function,
                                  tableSimple, intervalCount)
            newtonMethod(x, function, beta, tableNewton, intervalCount)
            scanningMethodVariableStep(
                x, x + step, step / 4, function, tableVariable, intervalCount)
        iteration = iteration + 1
        x = x + step
    print("Skenavimo metodo rezultatai")
    print(tableScan)
    print("Paprastų iteracijų metodo rezultatai")
    print(tableSimple)
    print("Niutono (liestinių) metodo rezultatai")
    print(tableNewton)
    print("Skenavimo su mažėjančiu žingsniu metodo rezultatai")
    print(tableScan)


def simpleIterationMethod(alpha, x, function, table, intervalCount):
    iteration = 0
    precision = 1000
    goal = 1e-8
    while precision > goal:
        iteration = iteration + 1
        fHat = x + function(x)/alpha
        x = fHat
        precision = np.abs(function(x))
    table.add_row([intervalCount, iteration, x, alpha, precision])


def newtonMethod(x, function, beta, table, intervalCount):
    iteration = 0
    precision = 1000
    goal = 1e-8
    while (precision > goal):
        iteration = iteration + 1
        fCurrent = function(x)
        derivativeOfCurrent = derivative(function, x, dx=0.0001)
        x = x - beta * fCurrent/derivativeOfCurrent
        precision = np.abs(function(x))
    table.add_row([intervalCount, iteration, x, beta, precision])


def scanningMethodVariableStep(xStart, xEnd, step, function, table, intervalCount):
    x = xStart
    iteration = 0
    precision = 1000
    goal = 1e-8
    while x < xEnd:
        iteration = iteration + 1
        if np.sign(function(x)) != np.sign(function(x+step)):
            step = step/4
        else:
            x = x + step
        precision = np.abs(function(x))
        if precision < goal:
            table.add_row([intervalCount, iteration, x, x + step, precision])
            break
        x = x + step


negativeEstimateFx = -4.5625
positiveEstimateFx = 4.182
startIntervalGx = 5
endIntervalGx = 10
h = 0.001
#scanningMethod(negativeEstimateFx, positiveEstimateFx, h, fx)
scanningMethod(startIntervalGx, endIntervalGx, h, gx)

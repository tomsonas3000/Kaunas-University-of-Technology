import matplotlib.pyplot as plt
import numpy as np
from scipy.misc import derivative


def fc(c):
    return (70*9.8/c)*(1-np.exp(-(c/70)*3)) - 27


def newtonMethod(c, function, beta):
    iteration = 0
    precision = 1000
    goal = 1e-8
    while (precision > goal):
        iteration = iteration + 1
        fCurrent = function(c)
        derivativeOfCurrent = derivative(function, c, dx=0.0001)
        c = c - beta * fCurrent/derivativeOfCurrent
        precision = np.abs(function(c))
    print(
        "Newton method: Iteration count : {0} x: {1}".format(iteration, c))
    return c


root = newtonMethod(-70, fc, 0.8)

c = np.linspace(-75, 10, 1000)
c0 = [-75, 10]

y = fc(c)
y0 = [0, 0]

plt.title('f(c)')
plt.plot(c, y)
plt.plot(c0, y0)
plt.plot(root, 0, markerSize=10, marker="x")
plt.show()

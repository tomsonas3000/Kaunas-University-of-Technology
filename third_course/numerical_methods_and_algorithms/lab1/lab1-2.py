import matplotlib.pyplot as plt
import numpy as np


def fx(x):
    return 0.48*x**5 + 1.71*x**4 - 0.67*x**3 - 4.86*x**2 - 1.33*x + 1.5


def gx(x):
    return np.exp(-x) * np.sin(x**2) + 0.001


negativeEstimate = -4.5625
positiveEstimate = 4.182

x = np.linspace(negativeEstimate, positiveEstimate, 1000)
x2 = np.linspace(-3.25, 2)
x0 = [negativeEstimate, 0, positiveEstimate]
x20 = [-3.25, 0, 2]

y = fx(x)
y2 = fx(x2)
y0 = [0, 0, 0]
y20 = [0, 0, 0]

plt.title('f(x)')
#plt.plot(x, y)
#plt.plot(x0, y0)
plt.plot(x2, y2)
plt.plot(x20, y20)
plt.xlabel('x axis')
plt.ylabel('y axis')
plt.show()

x = np.linspace(5, 10, 1000)
x0 = [5, 10]

y = gx(x)
y0 = [0, 0]

plt.plot(x, y)
plt.plot(x0, y0)
plt.title('g(x)')
plt.xlabel('x axis')
plt.ylabel('y axis')
plt.show()

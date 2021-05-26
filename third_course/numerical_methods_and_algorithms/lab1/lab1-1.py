import matplotlib.pyplot as plt
import numpy as np


def fx(x):
    return 0.48*x**5 + 1.71*x**4 - 0.67*x**3 - 4.86*x**2 - 1.33*x + 1.5


negativeEstimate = -4.5625
positiveEstimate = 4.182
x = np.linspace(-5, 5)
y = fx(x)
x0 = [negativeEstimate - 0.5, 0, positiveEstimate + 0.5]
y0 = [0, 0, 0]

plt.plot(x, y)
plt.plot(x0, y0)
plt.plot(negativeEstimate, 0, markerSize=10, marker="|")
plt.plot(positiveEstimate, 0, markerSize=10, marker="|")

plt.xlabel('x axis')
plt.ylabel('y axis')

plt.title('Saknu intervalas')

plt.show()

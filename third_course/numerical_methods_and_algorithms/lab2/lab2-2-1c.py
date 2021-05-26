import numpy as np


def f(x):
    return np.matrix([[x[0]**2 + 2*(x[1] - np.cos(x[0]))**2 - 20],
                      [x[0]**2*x[1] * 2]])


def df(x):
    return np.matrix([[2 * x[0] + 4 * np.sin(x[0]) * (x[1] - np.cos(x[0])), 4 * (x[1] - np.cos(x[0]))], [2*x[0]*x[1], x[0]**2]])


def Newton(x, alpha):
    startingX = x
    eps = 1e-3
    itmax = 1000
    for i in range(itmax):
        dfx = df(x)
        fx = f(x)
        deltax = np.linalg.solve(-dfx, fx)
        x1 = np.add(x, np.dot(alpha, deltax))
        x1AsArray = [x1[0, 0], x1[0, 1]]
        accuracy = np.linalg.norm(fx)
        print(accuracy)
        if accuracy < eps:
            print("Pradiniai artiniai" + str(startingX))
            print("Iteracijų skaičius: " + str(i+1))
            print("Gauti sprendiniai:" + str(x))
            break
        x = x1AsArray


alpha = 1
#x = [3.64495, -0.813]
#x = [-4.588, -0.1301]
x = [-1.7, -0.7]
Newton(x, alpha)

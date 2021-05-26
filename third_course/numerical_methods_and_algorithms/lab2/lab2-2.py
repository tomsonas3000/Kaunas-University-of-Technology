import numpy as np


def f(x):
    return np.matrix([[x[1]-x[2]+x[3]-1],
                      [5*x[0]+4*x[2]*x[3]+26],
                      [5*x[1]**3-x[2]**2+634],
                      [4*x[0]-3*x[1]-2*x[2]-17]])


def df(x):
    return np.matrix([[0, 1, -1, 1],
                      [5, 0, 4*x[3], 4*x[2]],
                      [0, 15*x[1]**2, -2*x[2], 0],
                      [4, -3, 2, 0]])


def Newton(x, alpha):
    eps = 1e-3
    itmax = 1050
    for i in range(itmax):
        dfx = df(x)
        fx = f(x)
        deltax = np.linalg.solve(-dfx, fx)
        x1 = np.add(x, np.dot(alpha, deltax))
        x1AsArray = [x1[0, 0], x1[0, 1], x1[0, 2], x1[0, 3]]
        accuracy = np.linalg.norm(fx)
        print(accuracy)
        if accuracy < eps:
            print("iteracija")
            print("sprendinys")
            print(x)
            break
        x = x1AsArray


alpha = 0.01
x = [3.37, -3.6, -2.3, 3]
Newton(x, alpha)

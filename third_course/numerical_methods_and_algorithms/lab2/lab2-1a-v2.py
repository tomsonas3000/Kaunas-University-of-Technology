
import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits import mplot3d
import math

# -------- Lygciu sistemos funkcija---------


def LF(x):  # grazina reiksmiu stulpeli
    s = np.matrix([[x[0]**2 + 2*(x[1] - np.cos(x[0]))**2 - 20],
                   [x[0]**2*x[1] * 2]])
    return s
# ----------------------------------


def Surface(X, Y, LF):
    size = np.shape(X)
    Z = np.zeros(shape=(size[0], size[1], 2))
    for i in range(0, size[0]):
        for j in range(0, size[1]):
            Z[i, j, :] = LF([X[i][j], Y[i][j]]).transpose()
    return Z


fig1 = plt.figure(1, figsize=plt.figaspect(0.5))
ax1 = fig1.add_subplot(1, 2, 1, projection='3d')
ax1.set_xlabel('x')
ax1.set_ylabel('y')
ax1.set_ylabel('z')
ax2 = fig1.add_subplot(1, 2, 2, projection='3d')
ax2.set_xlabel('x')
ax2.set_ylabel('y')
ax2.set_ylabel('z')
# plt.draw();  #plt.pause(1);
xx = np.linspace(-6, 6, 50)
yy = np.linspace(-6, 6, 50)
X, Y = np.meshgrid(xx, yy)
Z = Surface(X, Y, LF)

surf1 = ax1.plot_surface(X, Y, Z[:, :, 0], color='blue', alpha=0.4)
wire1 = ax1.plot_wireframe(
    X, Y, Z[:, :, 0], color='black', alpha=1, linewidth=0.3, antialiased=True)
surf2 = ax1.plot_surface(X, Y, Z[:, :, 1], color='purple', alpha=0.4)
wire2 = ax1.plot_wireframe(
    X, Y, Z[:, :, 1], color='black', alpha=1, linewidth=0.3, antialiased=True)
CS11 = ax1.contour(X, Y, Z[:, :, 0], [0], colors='b')
CS12 = ax1.contour(X, Y, Z[:, :, 1], [0], colors='g')
CS1 = ax2.contour(X, Y, Z[:, :, 0], [0], colors='b')
CS2 = ax2.contour(X, Y, Z[:, :, 1], [0], colors='g')

XX = np.linspace(-5, 5, 2)
YY = XX
XX, YY = np.meshgrid(XX, YY)
ZZ = XX*0
zeroplane = ax2.plot_surface(
    XX, YY, ZZ, color='gray', alpha=0.4, linewidth=0, antialiased=True)

plt.show()

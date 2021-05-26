import matplotlib.pyplot as plt
import numpy as np

def SplineCofs(X, Y):
    n = len(X)
    A = np.zeros((n, n))
    b = np.zeros((n, 1))
    d = X[1:n] - X[0:(n-1)]

    for i in range(0,n - 2):
        A[i,i:i+3]=[d[i]/6, (d[i]+d[i+1])/3,d[i+1]/6]
        b[i]=(Y[i+2]-Y[i+1])/d[i+1]-(Y[i+1]-Y[i])/d[i]
        if b[i] == float("inf"):
            b[i] = 1
        if b[i] == float("-inf"):
            b[i] = -1
    A[n-2, 0] = 1
    A[n-1,n-1] = 1
    cofs = np.linalg.solve(A,b)
    return cofs

def SplineValues(X,Y,cofs,n):
    d = X[1] - X[0]
    xPlot = np.linspace(X[0], X[1], n)
    S = cofs[0] / 2 * (xPlot - X[0])**2 + (cofs[1] - cofs[0]) / (6 * d) * (xPlot - X[0])**3 + (xPlot - X[0]) * ((Y[1] - Y[0]) / d - cofs[0] * d/3 - cofs[1] * d/6) + Y[0]
    return xPlot, S

xprad = np.array(np.loadtxt("./xmainv2.txt", delimiter=','))
yprad = np.array(np.loadtxt("./ymainv2.txt", delimiter=','))
xprad = xprad[0::5]
yprad = yprad[0::5]

#t = np.linspace(0, 20, len(xprad))

#ttt = np.linspace(0, 20, 1000)

cofs = SplineCofs(xprad, yprad)
for i in range(0,len(xprad) - 1):
    n = 2
    xPlot, S = SplineValues(xprad[i:i+2], yprad[i:i+2], cofs[i:i+2],n)
    plt.plot(xPlot, S, color="green")
    #plt.plot(ttt[i:i+2], xPlot, color="green")
    #plt.plot(ttt[i:i+2], S, color="green")

plt.title('Prancūzija')
plt.xlabel('x reikšmės')
plt.ylabel('y reikšmės')
plt.show()
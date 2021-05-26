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
    A[n-2, 0] = 1
    A[n-1,n-1] = 1
    cofs = np.linalg.solve(A,b)
    return cofs

def SplineValues(X,Y,cofs,n):
    d = X[1] - X[0]
    xPlot = np.linspace(X[0], X[1], n)
    S = cofs[0] / 2 * (xPlot - X[0])**2 + (cofs[1] - cofs[0]) / (6 * d) * (xPlot - X[0])**3 + (xPlot - X[0]) * ((Y[1] - Y[0]) / d - cofs[0] * d/3 - cofs[1] * d/6) + Y[0]
    return xPlot, S

xprad = np.array([1,2,3,4,5,6,7,8,9,10,11,12])
yprad = np.array([4.3428, 4.71047, 6.44371, 9.99282, 13.1694, 18.0087, 18.9351, 19.7021, 17.2087, 13.2821, 6.92157, 3.88708])
cofs = SplineCofs(xprad, yprad)
for i in range(0,11):
    dotCount = 50
    xPlot, S = SplineValues(xprad[i:i+2], yprad[i:i+2], cofs[i:i+2],dotCount)
    plt.plot(xPlot,S, color="green")

plt.title('Prancūzija 2004 temperatūros')
plt.xlabel('x reikšmės')
plt.ylabel('y reikšmės')
plt.plot(xprad,yprad,color='red',marker='o',linestyle='')
plt.show()
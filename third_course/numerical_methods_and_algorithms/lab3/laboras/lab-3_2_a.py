import matplotlib.pyplot as plt
import numpy as np

#Apskaičiuoja daugianarių koeficientus
def NewtonInterpolation(x,y):
    n = len(x)
    A = np.zeros((n,n+1))
    A[:,0]= x[:]
    A[:,1]= y[:]
    for j in range(2,n+1):
        for i in range(j-1,n):
            A[i,j] = (A[i,j-1]-A[i-1,j-1]) / (A[i,0]-A[i-j+1,0])
    p = np.zeros(n)
    for k in range(0,n):
        p[k] = A[k,k+1]
    return p

#Apskaičiuoja daugianario reikšmes duotuose taškuose
def poly(t,x,p):
    n = len(x)
    out = p[n-1]
    for i in range(n-2,-1,-1):
        out = out*(t-x[i]) + p[i]
    return out

xprad = [1,2,3,4,5,6,7,8,9,10,11,12]
yprad = [4.3428, 4.71047, 6.44371, 9.99282, 13.1694, 18.0087, 18.9351, 19.7021, 17.2087, 13.2821, 6.92157, 3.88708]

a = NewtonInterpolation(xprad, yprad)

x = np.linspace(1, 12, 1000)
yval = poly(x, xprad, a)

plt.plot(x,yval,color='green',linestyle='-',label='interpoliacinė')
plt.title('Prancūzija 2004 temperatūros')
plt.xlabel('Mėnesis')
plt.ylabel('Temperatūra')
plt.legend(loc='best')
plt.plot(xprad,yprad,color='red',marker='o',linestyle='')
plt.show()
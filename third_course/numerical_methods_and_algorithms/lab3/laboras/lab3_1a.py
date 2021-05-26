import matplotlib.pyplot as plt
import numpy as np

def func(x):
    return np.cos(2 * x) * (np.sin(2 * x) + 1.5) + np.cos(x)

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

n = 5
I = np.array(range(0,n))
xmin = -2
xmax = 3

xpt = (xmax-xmin) / 2 * np.cos(np.pi * (2*I+1) / (2*n) ) + (xmax+xmin) / 2 #Čiobyševo
#xpt = np.linspace(xmin,xmax, n) #Tolygiai išsidėstę x
ypt = func(xpt)

a = NewtonInterpolation(xpt,ypt)

x = np.linspace(xmin,xmax,1000)
yprad = func(x)
yval = poly(x,xpt,a)
ynetikt = yprad - yval

plt.plot(x,yval,color='green',linestyle='-',label='interpoliacinė')
plt.plot(x,yprad,color='blue', linestyle='-', label='pradine')
plt.plot(x, ynetikt, color='red', linestyle='-', label='netiktis' )
plt.title('Niutono interpoliacija')
plt.xlabel('x reikšmės')
plt.ylabel('y reikšmės')
plt.legend(loc='best')
plt.plot(xpt,ypt,color='red',marker='o',linestyle='')
plt.show()
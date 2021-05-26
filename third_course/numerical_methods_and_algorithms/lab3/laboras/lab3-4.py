import matplotlib.pyplot as plt
import numpy as np

xprad = np.array([1,2,3,4,5,6,7,8,9,10,11,12])
yprad = np.array([4.3428, 4.71047, 6.44371, 9.99282, 13.1694, 18.0087, 18.9351, 19.7021, 17.2087, 13.2821, 6.92157, 3.88708])
n = len(xprad)
m = 5
G = np.array(np.linspace(1, 1, n*m)).reshape(n,m)
for i in range(0,m):
    G[:, i] = xprad**(i)
GT = G.transpose()
a = np.dot(GT,G)
b = np.dot(GT,yprad)
c = np.linalg.solve(a, b)
sss = str(c[0])
for i in range(1,m):
    string = ' + {c}x^{i}'.format(c=c[i],i=i)
    sss = sss + string
sss = sss.replace("+ -", "- ")
print(sss)

xxx=np.linspace(1,12,200)
G2 = np.array(np.linspace(1, 1, 200*m)).reshape(200,m)
for i in range(0,m):
    G2[:, i] = xxx**(i)
fff = np.dot(G2,c)

plt.plot(xprad,yprad,color='red',marker='o',linestyle='')
plt.plot(xxx,fff,color="green", label="aproksimuojanti kreive")
plt.legend(loc='best')
plt.show()
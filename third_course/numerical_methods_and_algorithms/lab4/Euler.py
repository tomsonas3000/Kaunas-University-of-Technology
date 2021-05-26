import numpy as np
from matplotlib import pyplot as plt

def df1(m1, m2, g, k, v):
    return (((m1 + m2)*g) - (k*v**2)) / (m1 + m2)

m1 = 125 #parasiutininko mase
m2 = 25 #irangos mase
h0 = 2000 #pradinis aukstis
v0 = 0 #pradinis greitis
tg = 20 #laisvo kritimo laikas
k1 = 0.5 #oro pasipriesinimas laisvo
k2 = 10 #oro pasipriesinimas isskleidus
g = 9.8 #laisvo kritimo pagreitis

Delta = np.array([0.005, 0.01, 0.125, 0.25])

for dt in Delta:  
    itermax = 1000000
    T=[]
    H=[]
    V=[]
    H.append(h0)
    V.append(v0)
    T.append(0)
    isOpen = 0
    for i in range(itermax):
        t = i * dt
        H.append(H[i] - dt * V[i])
        if (tg > t):
            V.append(V[i] + dt * df1(m1, m2, g, k1, V[i]))
        else:
            if isOpen == 0:
                print("Isiskleide parasiutas")
                print(H[i + 1])
                isOpen = -1
            V.append(V[i] + dt * df1(m1, m2, g, k2, V[i]))
        T.append(t)
        if (H[i+1] < 0):
            print("delta reiksme")
            print(dt)
            print("greitis nusileidus")
            print(V[i + 1])
            print("po kiek nusileido")
            print(T[i + 1])
            print("------------")
            break
    plt.plot(T, V, label="Delta x: " + str(dt))   
    plt.grid()
plt.legend()
plt.show() 


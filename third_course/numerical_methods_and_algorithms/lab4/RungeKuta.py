import numpy as np
from matplotlib import pyplot as plt

m1 = 125 #parasiutininko mase
m2 = 25 #irangos mase
h0 = 2000 #pradinis aukstis
v0 = 0 #pradinis greitis
tg = 20 #laisvo kritimo laikas
k1 = 0.5 #oro pasipriesinimas laisvo
k2 = 10 #oro pasipriesinimas isskleidus
g = 9.8 #laisvo kritimo pagreitis
Delta = np.array([0.005, 0.01, 0.125, 0.25])

def df1(m1, m2, g, k, v):
    return (((m1 + m2)*g) - (k*v**2)) / (m1 + m2)

for dt in Delta:  
    itermax = 100000
    T=[]
    H=[]
    V=[]
    H.append(h0)
    V.append(v0)
    T.append(0)
    isOpen = 0
    for i in range(itermax):
        t = i * dt
        y = V[i]
        H.append(H[i] - dt * V[i])
        if (tg > t):
           dy = df1(m1, m2, g, k1, V[i])
           yz = y + dt/2*dy
           dyz = df1(m1, m2, g, k1, yz)
           yzz = y + dt/2*dyz
           dyzz = df1(m1, m2, g, k1, yzz)
           yzzz = y + dt/2*dyzz
           dyzzz = df1(m1, m2, g, k1, yzzz)
           y = y + dt/6*(dy+2*dyz+2*dyzz+dyzzz)
        else:
            if isOpen == 0:
                isOpen = -1  
                print("Isiskleide parasiutas")
                print(H[i + 1])
            dy = df1(m1, m2, g, k2, V[i])
            yz = y + dt/2*dy
            dyz = df1(m1, m2, g, k2, yz)
            yzz = y + dt/2*dyz
            dyzz = df1(m1, m2, g, k2, yzz)
            yzzz = y + dt/2*dyzz
            dyzzz = df1(m1, m2, g, k2, yzzz)
            y = y + dt/6*(dy+2*dyz+2*dyzz+dyzzz)
        V.append(y)
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


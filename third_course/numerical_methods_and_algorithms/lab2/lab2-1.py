import numpy as np

A = np.array([[2, 5, 1, 2],
              [-2, 0, 3, 5],
              [1, 0, -1, 1],
              [0, 5, 4, 7]]).astype(np.float)
B = np.array([[-1], [7], [3], [4]]).astype(np.float)
# A = np.array([[1, 1,  1,  1],
#              [1, -1, -1,  1],
#              [2,  1, -1,  2],
#              [3,  1,  2, -1]]).astype(np.float)
# B = np.array([[2], [0], [9], [7]]).astype(np.float)
n = (np.shape(A))[0]
nb = (np.shape(B))[0]
# print(n)
C = np.hstack((A, B))
# print(C)
# A[[0, 2], :] = A[[2, 0], :]
# print("\n")
# print(A)
# print("\n")
# ixgrid = np.ix_([0, 1, 2, 3], [0])
# print(np.amax(A[ixgrid]))  # max value
# print(np.where(A[ixgrid] == np.amax(A[ixgrid]))[0])  # max value index
# print(A)
for i in range(0, n-1):
    print(C)
    print("\n" + str(i+1) + " pertvarkymas\n")
    # print("\n")
    # print("--------------------")
    for j in range(i+1, n):
        # print("pries")
        # print(C)
        # print("\n")
        # print(C[j, i:n+1])
        # print(C[j, i])
        # print(C[i, i])
        C[j, i:n+1] = C[j, i:n+1]-C[i, i:n+1]*C[j, i]/C[i, i]
        # print("po")
        # print(C)
print("   x1   x2   x3   x4   y")
print(C)

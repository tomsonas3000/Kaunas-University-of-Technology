class Traveller:
    def __init__(self, name):
        self.name = name
        self.balance = 0

    def __eq__(self, other):
        return self.name == other.name

    def __lt__(self, other):
        return self.balance < -other.balance

    @staticmethod
    def solve():
        while True:
            positive, negative = 0, 0
            # find a positive balance
            for positive in range(len(travellers)):
                if travellers[positive].balance > 0:
                    break
            # find a negative balance
            for negative in range(len(travellers)):
                if travellers[negative].balance < 0:
                    break
            if positive + 1 == len(travellers) and negative + 1 == len(travellers):
                break
            # check to not subtract to a negative number
            if travellers[positive] < travellers[negative]:
                difference = travellers[positive].balance
            else:
                difference = (-travellers[negative].balance)
            # balance balances
            travellers[positive].balance -= difference
            travellers[negative].balance += difference
            print(travellers[positive].name + " " + travellers[negative].name + " " + str(difference))

    @staticmethod
    def read():
        n, t = input().split()  # number of travellers, number of transactions
        nAsInt = int(n)
        tAsInt = int(t)
        if nAsInt == 0 and tAsInt == 0:
            quit()
        for i in range(nAsInt):
            newName = input()
            travellers.append(Traveller(newName))
        for i in range(tAsInt):
            name1, name2, amount = input().split()
            if travellers.index(Traveller(name1)) is not None and travellers.index(Traveller(name2)) is not None:
                travellers[travellers.index(Traveller(name1))].balance -= int(
                    amount)  # subtract from one balance and add to other balance
                travellers[travellers.index(Traveller(name2))].balance += int(amount)
        print("Case #{case}".format(case=caseCount))


if __name__ == '__main__':
    caseCount = 0  # which case
    while True:
        caseCount = caseCount + 1
        travellers = []
        Traveller.read()
        Traveller.solve()
        print()

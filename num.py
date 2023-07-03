# Gramatica: E -> T X
#           X -> + E | - E | vazio
#           T -> F Y
#           Y -> * F | / F | vazio
#           F -> ( E ) | 0



def le_entrada():
    n = str(input("Digite a entrada: "))
    n = n + '$'
    return n


entrada = le_entrada()
i = 0
LA = entrada[i]

# Match
def match(t):
    global i
    global LA
    if LA == t:
        i = i + 1
        LA = entrada[i]
    else:
        print("ERRO: ", LA, " não é um token válido, esperava ", t)
        exit()
    

def E():
    print("Estado: E LA: ", LA)
    T()
    X()

def X():
    print("Estado: X LA: ", LA)
    if LA == "+":
        match("+")
        E()
    elif LA == "-":
        match("-")
        E()
    else:
        if LA != "$" and LA != ")":
            print("ERRO: ", LA, " não é um token válido, esperava + ou -")
            exit()

def T():
    print("Estado: T LA: ", LA)
    F()
    Y()

def Y():
    print("Estado: Y LA: ", LA)
    if LA == "*":
        match("*")
        E()
    elif LA == "/":
        match("/")
        E()
    else:
        if LA != "$" and LA != ")" and LA != "+" and LA != "-":
            print("ERRO: ", LA, " não é um token válido, esperava * ou /")
            exit()  

def F():
    print("Estado: F LA: ", LA)
    if LA == "(":
        match("(")
        E()
        match(")")
    elif LA == "0":
        match("0")
    else:
        if LA != "$" and LA != ")" and LA != "+" and LA != "-" and LA != "*" and LA != "/":
            print("ERRO: ", LA, " não é um token válido, esperava ( ou 0")
            exit()


E()
if LA == "$":
    print("Aceito")
else:
    print("Não aceito")
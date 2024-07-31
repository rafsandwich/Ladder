# pokemon game mockup written up on a whim after my partner suggested it

import random

class Lad:
    def __init__(self, name, hp, spd, move1, move2):
        self.name = name
        self.hp = hp
        self.spd = spd
        #self.atk = atk
        self.moves = {
            "Move1": move1,
            "Move2": move2
        }

    def attack(self, other):
        move = random.choice(list(self.moves.keys()))
        damage = self.moves[move]
        other.hp -= damage

        print(f"{self.name} used {move} on {other.name}, hitting for {damage} damage!")
        print(f"{other.name} | HP: {other.hp}")

    def is_dead(self):
        return self.hp <= 0

    def __str__(self):
        return f"{self.name} | HP = {self.hp}, Speed = {self.spd}, Moves = {self.moves}"

lad1 = Lad(name="Fish", hp=15, spd=10, move1=13, move2=6)
lad2 = Lad(name="Jess", hp=22, spd=5, move1=8, move2=11)

print(lad1)
print(lad2)

while lad1.hp >= 0 and lad2.hp >= 0:
    if lad1.spd > lad2.spd:
        lad1.attack(lad2)
        if not lad2.is_dead():
            lad2.attack(lad1)

    else:
        lad2.attack(lad1)
        if not lad1.is_dead():
            lad1.attack(lad2)

if lad1.hp <= 0:
    print(lad2.name + " wins!")
elif lad2.hp <=0:
    print(lad1.name + " wins!")

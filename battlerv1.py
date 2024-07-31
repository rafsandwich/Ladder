# hi, this is a pokemon game mock up

class Lad:
    def __init__(self, name, hp, speed):
        self.name = name
        self.hp = hp
        self.speed = speed

    def attack(self, other):
        damage = 10
        other.hp -= damage

        print(f"{self.name} attacked {other.name}, hitting for {damage} damage!")
        print(f"{other.name}'s hp is now {other.hp}")

    def is_dead(self):
        return self.hp <= 0

    def __str__(self):
        return f"{self.name} | HP = {self.hp}, Speed = {self.speed}"

lad1 = Lad(name="Fish", hp=15, speed=10)
lad2 = Lad(name="Jess", hp=22, speed=5)

print(lad1)
print(lad2)

while lad1.hp >= 0 and lad2.hp >= 0:
    if lad1.speed > lad2.speed:
        lad1.attack(lad2)
        if not lad2.is_dead():
            lad2.attack(lad1)

    else:
        lad2.attack(lad1)
        if not lad1.is_dead():
            lad1.attack(lad2)

if lad1.hp <= 0:
    print(lad2.name + " wins!")

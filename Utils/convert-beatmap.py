import random
import json

file_to_read = input("File to convert: ")
file = open(file_to_read, "r")

data = []

for line in file:
    ms_time = float(line) * 1000
    direction = "left"
    direction_rand = random.randint(1, 12)
    if direction_rand % 2 == 0:
        direction = "left"
    else:
        direction = "right"
    
    data.append({
        "type": "beat",
        "timestamp": ms_time,
        "direction": direction
    })

file.close()

output = json.dumps({
    "beatmapData": data
})

file = open(file_to_read + ".json", "w")
file.write(output)
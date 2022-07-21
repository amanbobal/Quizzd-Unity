import requests

seed = ["aman", "dumbs"]

for x in range(len(seed)):
    response = requests.get(
        "https://avatars.dicebear.com/api/bottts/{}.png".format(seed[x]))

    file = open("{}.png".format(seed[x]), "wb")
    file.write(response.content)
    file.close()

import urllib

fhand = urllib.urlopen('http://www.py4inf.com/')

 
for line in fhand:
    print line.strip()
 

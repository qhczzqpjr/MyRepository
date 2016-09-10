#coding=utf-8
import urllib
import re

def getHtml(url):
    print 'Getting html source code...'
    page = urllib.urlopen(url)
    html = page.read()
    return html

def getImg(html):
    print 'Getting all address of images...'
    reg = r'src="(.*\.jpg)"'
    imgre = re.compile(reg)
    imglist = re.findall(imgre,html)
    print 'Downloading...'
    x = 0
    for imgurl in imglist:
        urllib.urlretrieve(imgurl,'%s.jpg' % x)
        x+=1


html = getHtml("http://jandan.net/ooxx");

print getImg(html)

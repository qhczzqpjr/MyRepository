#! usr/bin/python
#coding=utf-8
import urllib
from BeautifulSoup import *

imglist =[]
j = 0
for i in range(1965,1966):
    url = 'http://jandan.net/ooxx/page-'+str(i)+'#comments'
    html = urllib.urlopen(url).read()
    soup = BeautifulSoup(html)
    imgtag = soup.findAll('img')
    print 'Downloading %s' % i
    for img in imgtag:
        j = j+1
        
        urllib.urlretrieve(img['src'],'G:\GitHub\MyRepository\Python\ImgDownloaded\%s-%s.jpg' % (i,j))
    
print 'finished'

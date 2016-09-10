#! usr/bin/python
# -*- coding:utf-8 -*-
# Export LOL Champions Info
import urllib
import codecs
from BeautifulSoup import *

url = 'http://lol.duowan.com/hero/'#raw_input('Enter url: ')

html = urllib.urlopen(url).read()

soup = BeautifulSoup(html)

heroInfo = []


champions = soup.find(id=u'champion_list').findAll(u'li')
if champions !=None:
    for champion in champions:
        heronamecn = champion.find('div',{u'class':u'champion_name'}).string
        print 'Process Start:' + unicode(heronamecn)
        #sublevel
        url2 = champion.find(u'a',{u'class':u'lol_champion'}).get(u'href',None)
        html2 = urllib.urlopen(url2).read()
        ssoup = BeautifulSoup(html2)
        #print 'Process Start: '+url2
        r = re.search(r'com/heros/(\w+)/',url2)
        if r!= None:
            heronameen = r.group(1)
            itemlist = []
            ulroot = ssoup.find(u'div',{u'class':u'hero-sz'}).ul
            for o in ulroot.findAll(u'li'):
                itemlist.append({u'ItemName':unicode(o.p.string),'Itemvalue':unicode(o.span.string)})
                #print o.p.string+o.span.string
            heroInfo.append({'HeroEnglishName':heronameen,'HeroChineseName':heronamecn,'ItemInfo':itemlist})
            print "Process Succss"
        else:
            r = re.search(r'com/(\w+)/',url2)
            if r==None:
                break;
            else:
                heronameen = r.group(1)
                itemlist = []
                ulroot = ssoup.find(u'div',{u'class':u'hero-box ext-attr'}).find(u'div',{u'class':u'hero-box__bd'}).ul
                for o in ulroot.findAll(u'li'):
                    itemlist.append({u'ItemName':unicode(o.em.string),u'Itemvalue':unicode(o.span.string)})
                    #print o.em.string+o.span.string
                    #print unicode(o.em.string)+unicode(o.span.string)
                heroInfo.append({u'HeroEnglishName':heronameen,u'HeroChineseName':heronamecn,u'ItemInfo':itemlist})
                print "Process Succss"
                
        
        

#sublevel
#url2 = 'http://lol.duowan.com/heros/sol/'


#print str(heroInfo).replace('u\'','\'').decode('unicode-escape')
output = codecs.open('result.txt','w','utf-8')
output.write(str(heroInfo).replace('u\'','\'').decode('unicode-escape'))
output.close()

# print ssoup.findAll('div',{'class':'fl'})

 

#Get all the links but without chinese name
#tags = soup.findAll('a',{'class':'lol_champion'})
#for tag in tags:
#    print tag.get('href',None)

#tags = soup.findAll('div',{'class':'champion_name'})
#for tag in tags:
#    print tag.text

#Retrive the root tag
#tag = soup.find(id='champion_list')
#print tag.li.a['href']
#Only can get value when it is NavigableString otherwise return null

#for li in tag:
#    if li.string != None:
#        print li
#    else:
#        print li.contents[0]
    
#print li.div.string
#print li.div.contents[0]

#print tag.li.div.string
#print tag.li.div.contents[0]
#print type(tag.li.a)



#get all the href with noise
#tags = soup('a')
#for tag in tags:
    #print tag.find('a').get('href',None)
    #print tag.find('div')

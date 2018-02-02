# This program generates a set of images for user to classify
# The task is designed for Stroop effect
# Python 'pillow' package is required

from PIL import Image, ImageDraw, ImageFont
from random import *
import os

participant = 'example' # participant number


congruent_mode = False # color and word are congruent?

pool = ['RED','GREEN','BLUE','YELLOW','PURPLE','ORANGE']
hints = ['COLOR','WORD']
W, H = (666,400)

# create participant' task
if congruent_mode:
	ppath = participant+'/congruent/'
else:
	ppath = participant+'/incongruent/'
if not os.path.exists(ppath):
	os.makedirs(ppath+'source')
	for item in pool:
		os.makedirs(ppath+'target/'+item)
	
# this file contains the correct answers
f = open(ppath+'ans.txt','w')

for num in range(0, 100):

	color_idx = randint(0, len(pool)-1)
	if congruent_mode:
		word_idx = color_idx
	else:
		word_idx = randint(0, len(pool)-1)
	hint_idx = randint(0, 1)
	
	color = pool[color_idx]
	word = pool[word_idx]
	hint = hints[hint_idx]

	img = Image.new('RGB', (W, H),color = 'white')
	fnt = ImageFont.truetype('font/OpenSans-Bold.ttf', 150)
	fnt_hint = ImageFont.truetype('font/OpenSans-Regular.ttf', 40)

	d = ImageDraw.Draw(img)
	# draw in the middle
	w, h = d.textsize(word,font=fnt)
	d.text(((W-w)/2,(H-h)/2), word, fill=color,font=fnt)
	if not congruent_mode:
		d.text((10,10), hint, fill='black',font=fnt_hint)

	img.save(ppath+'source/'+str(num)+'.png')
	
	if hint_idx == 0:
		f.write(str(num)+'.'+color+'\n')
	else:
		f.write(str(num)+'.'+word+'\n')
		
f.close()



##################################
# same participant, different mode
##################################

congruent_mode = True # color and word are congruent?

pool = ['RED','GREEN','BLUE','YELLOW','PURPLE','ORANGE']
hints = ['COLOR','WORD']
W, H = (600,400)

# create participant' task
if congruent_mode:
	ppath = participant+'/congruent/'
else:
	ppath = participant+'/incongruent/'
if not os.path.exists(ppath):
	os.makedirs(ppath+'source')
	for item in pool:
		os.makedirs(ppath+'target/'+item)
	
# this file contains the correct answers
f = open(ppath+'ans.txt','w')

for num in range(0, 100):

	color_idx = randint(0, len(pool)-1)
	if congruent_mode:
		word_idx = color_idx
	else:
		word_idx = randint(0, len(pool)-1)
	hint_idx = randint(0, 1)
	
	color = pool[color_idx]
	word = pool[word_idx]
	hint = hints[hint_idx]

	img = Image.new('RGB', (W, H),color = 'white')
	fnt = ImageFont.truetype('font/OpenSans-Bold.ttf', 150)
	fnt_hint = ImageFont.truetype('font/OpenSans-Regular.ttf', 40)

	d = ImageDraw.Draw(img)
	# draw in the middle
	w, h = d.textsize(word,font=fnt)
	d.text(((W-w)/2,(H-h)/2), word, fill=color,font=fnt)
	if not congruent_mode:
		d.text((10,10), hint, fill='black',font=fnt_hint)

	img.save(ppath+'source/'+str(num)+'.png')
	
	if hint_idx == 0:
		f.write(str(num)+'.'+color+'\n')
	else:
		f.write(str(num)+'.'+word+'\n')
		
f.close()
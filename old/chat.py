# -*- coding: utf-8 -*- 

import socket
import tkinter as tk
from tkinter import ttk
from importlib import reload  # Python 3.4+
import sys

#Решаем вопрос с кирилицей
reload(sys)
#sys.setdefaultencoding('utf-8')
#-----------------------------

def updateScrollRegion():
	cTableContainer.update_idletasks()
	cTableContainer.config(scrollregion=fTable.bbox())
def createScrollableContainer():
	cTableContainer.config(xscrollcommand=sbHorizontalScrollBar.set,yscrollcommand=sbVerticalScrollBar.set, highlightthickness=0)
	#sbHorizontalScrollBar.config(orient=tk.HORIZONTAL, command=cTableContainer.xview)
	sbVerticalScrollBar.config(orient=tk.VERTICAL, command=cTableContainer.yview)

	#sbHorizontalScrollBar.pack(fill=tk.X, side=tk.BOTTOM, expand=tk.FALSE)
	sbVerticalScrollBar.pack(fill=tk.Y, side=tk.RIGHT, expand=tk.FALSE)
	cTableContainer.pack(fill=tk.BOTH, side=tk.LEFT, expand=tk.TRUE)
	cTableContainer.create_window(0, 0, window=fTable, anchor=tk.NW)
	
#def on_configure(event):
    # update scrollregion after starting 'mainloop'
    # when all widgets are in canvas
#    canvas.configure(scrollregion=canvas.bbox('all'))

#tk=Tk()
root = tk.Tk()

# --- create canvas with scrollbar ---

#canvas = Canvas(tk)
#canvas.pack(side=LEFT)
cTableContainer = tk.Canvas(root)
fTable = tk.Frame(cTableContainer)

sbHorizontalScrollBar = tk.Scrollbar(root)
sbVerticalScrollBar = tk.Scrollbar(root)
#scrollbar = Scrollbar(tk, command=canvas.yview)
#scrollbar.pack(side=LEFT, fill='y')

#canvas.configure(yscrollcommand = scrollbar.set)

# update scrollregion after starting 'mainloop'
# when all widgets are in canvas
#canvas.bind('<Configure>', on_configure)

# --- put frame in canvas ---

#frame = Frame(canvas)

#canvas.create_window((0,0), window=frame, anchor='nw')

# Vertical (y) Scroll Bar
#scroll = Scrollbar(tk)
#scroll.pack(side=RIGHT, fill=Y)
#canvas = Canvas(tk)
#v=Scrollbar(tk, orient='vertical')
#v.pack(side=RIGHT, fill='y')
s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
s.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
s.bind(('0.0.0.0',11719))

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST,1)

socket_server = socket.socket()
server_host = tk.StringVar()
#ip = socket.gethostbyname(server_host)
sport = 9090
#socket_server.connect((server_host, sport))

enabled = tk.IntVar()
text=tk.StringVar()
name=tk.StringVar()

name.set('HabrUser')
text.set('')
root.title('Echo')
root.geometry('400x300')

#frame = Frame(tk)
#scrolly = Scrollbar(tk, orient='vertical',command=canvas.yview)

def checkbutton_changed():
     if enabled.get() == 1:
         global s
         s = socket_server
         s.connect((host.get().split(':')[0], int(host.get().split(':')[1])))
         s.send(nick.get().encode())
    # else:
        # socket_server = socket.socket()
        # server_host = socket.gethostname()
        # #ip = socket.gethostbyname(server_host)
        # sport = 8080
        # socket_server.connect((server_host, sport))

#log = Text(tk, yscrollcommand=v.set)
#log.insert("1.0", "text\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\ntext\n")
#for i in range(20log):
#   log.insert(END, "Welcome to Tutorialspoint...\n")
nick = tk.Entry(root, textvariable=name)
msg = tk.Entry(root, textvariable=text)
host = tk.Entry(root, textvariable=server_host)
cb = ttk.Checkbutton(text="Использовать TCP подключение", variable=enabled, command=checkbutton_changed)
#btn = ttk.Button(text="Подключится", command=checkbutton_changed)
msg.pack(side='bottom', fill='x', expand='false')
cb.pack(side='bottom', fill='x', expand='false')
host.pack(side='bottom', fill='x', expand='false')
tk.Label(text="Введите адрес сервера (IP и порт):").pack(side='bottom', fill='x', expand='false')
nick.pack(side='bottom', fill='x', expand='false')
tk.Label(text="Введите имя").pack(side='bottom', fill='x', expand='false')
#v.config(command=log.yview)
#log.pack(side='left', fill='both',expand='true')

#scrollbar = ttk.Scrollbar(orient="vertical", command=log.yview)
#scrollbar.pack(side=RIGHT, fill=Y)

 
#log["yscrollcommand"]=scrollbar.set
i=0
def loopproc():
        #log.see(END)
        s.setblocking(False)
        
        try:                                                                                     
                #if enabled.get() == 1:
                    message = s.recv(128)
                    
                #else:
                #    message = socket_server.recv(128)
                    #log.insert(END,message.decode()+'\n')
                    #m = Label(text=message, justify=LEFT, wraplength=250, bd=4, bg='white')
                    #m.pack(pady=5)
                
                    global i
                    tk.Label(fTable, text=message.decode(), bg='white').grid(row=i, pady=5)#, column=i)
                    i+=1
                
                    #canvas.create_window(0, 200, anchor='nw', window=m, height=50)
                    #scrollbar.config(m.yview)
                    #scrollbar.config(command=m.yview)
        except:
                updateScrollRegion()
                root.after(1,loopproc)
                return
        updateScrollRegion()
        root.after(1,loopproc)
        return

def sendproc(event):
    if enabled.get() == 1:
        if text.get().split()[0] == '/msg':
           log.insert(END,text.get()+'\n')
        socket_server.send(str.encode(name.get()+': '+text.get()))
        text.set('')
    else:
        sock.sendto (str.encode(name.get()+': '+text.get()),('255.255.255.255',11719))
        text.set('')
            


msg.bind('<Return>',sendproc)

msg.focus_set()

root.after(1,loopproc)
#canvas.configure(scrollregion=canvas.bbox('all'), yscrollcommand=scrolly.set)
#canvas.pack(side='top', fill='both',expand='true')
#scrolly.pack(side = RIGHT, fill=Y)
#frame.pack()
#scroll.config(command=log.yview)
createScrollableContainer()
root.mainloop()

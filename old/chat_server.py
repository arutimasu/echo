#!/usr/bin/env python
# -*- coding: utf-8 -*-

#import socket
from socket import *
import sys
from _thread import start_new_thread

port = 9090
if len(sys.argv) > 1:
        port = sys.argv[1]
clients = []
sock = socket(AF_INET, SOCK_STREAM) #socket() 
sock.bind(('', port))
sock.listen(5)
index = 0

def threaded(client, index):
     global clients
     while True:
        try:
                 data = client['connection'].recv(128)
                 print(data.decode())
                 if not data:
                     break
                 for i in clients:
                     #i['connection'].send(data.upper())
            
                     if data.decode().split()[1] == '/msg' and data.decode().split()[2] == i['name']:
                         i['connection'].send(b" ".join([data.split()[0], data.split()[3]]))
                     elif data.decode().split()[1] != '/msg' and data.decode() != "":
                         i['connection'].send(data)
        except:
                #код обработки ошибки
                print("Error")
                
             #clients.pop(index)
             #print(clients)
             #client['connection'].close()
                 
        #print(client['name'], " left the server")
 
while True:
        conn, addr = sock.accept()
    
   
        print(clients)
        print('connected:', addr)
        name = conn.recv(128)
        print(name.decode())
        #conn.sendall(str.encode(name.decode() + ' has joined to chat'))
        client = {
             'name': name.decode(),
             'connection': conn
         }
        clients.append(client)
        for i in clients:
                i['connection'].send(str.encode(name.decode() + ' has joined to chat'))
        start_new_thread(threaded, (client,index))
        index+=1
        
        
##serversocket = socket(AF_INET, SOCK_STREAM)
##try :
##    serversocket.bind(('localhost',9000))
##    serversocket.listen(5)
##    while(1):
##        (clientsocket, address) = serversocket.accept()
##
##        rd = clientsocket.recv(5000).decode()
##        if not 'HTTP' in rd:
##            #print(clients)
##            print('connected:', address)
##            name = clientsocket.recv(128)
##            print(name.decode())
##            client = {
##            'name': name.decode(),
##            'connection': clientsocket
##            }
##            clients.append(client)
##            for i in clients:
##                i['connection'].send(str.encode(name.decode() + ' has joined to chat'))
##            #start_new_thread(threaded, (conn,))
##        pieces = rd.split("\n")
##        if ( len(pieces) > 0 ) : print(pieces[0])
##
##        data = "HTTP/1.1 200 OK\r\n"
##        data += "Content-Type: text/html; charset=utf-8\r\n"
##        data += "\r\n"
##        data += "<html><body>Hello World</body></html>\r\n\r\n"
##        clientsocket.sendall(data.encode())
##        clientsocket.shutdown(SHUT_WR)
##
##except KeyboardInterrupt :
##    print("\nShutting down...\n");
##except Exception as exc :
##    print("Error:\n");
##    print(exc)
##
##serversocket.close()
##
##print('Access http://localhost:9000')
#createServer()


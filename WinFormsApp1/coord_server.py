import socket

sock = socket.socket()

sock.bind(('', 9090))
sock.listen(5)
addrs = []
#addrs.append('blank')
print("Coordinating server running")
while True:
    conn, addr = sock.accept()
    #conn.send('\n'.join(addrs).encode())
    data = conn.recv(27+10)
    message = data.decode()
    if message == 'get_users':
        conn.send('\n'.join(addrs).encode())
    else:
        addrs.append(message)
    print(message)
    
    conn.close()


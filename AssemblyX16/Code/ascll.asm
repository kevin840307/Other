code SEGMENT
	ASSUME cs:code,ds:code
	ORG	100h
start: jmp begin
begin:
	mov cx, 256
	mov dl, 0
next:
	mov ah, 2
	int 21h
	inc dl
	loop next
	mov cx, 10
	mov dl,48
next2:
	mov ah, 2
	int 21h
	inc dl
	loop next2
	mov ax, 4c00h
	int 21h
code ENDS
	END	start
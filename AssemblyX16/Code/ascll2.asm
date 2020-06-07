code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h
start:  mov bx, 0
next:	mov dl, bl
		mov cl, 4
		shr dl, cl
		call print
		mov dl, bl
		and dl, 0fh
		call print

		mov dl, ' '
		call p_ascii
		mov dl, bl
		call p_ascii
		mov dl, 0ah
		call p_ascii

		inc bl
		mov ch,20		;設定除數
        mov ax, bx		;設定被除數
        div ch			;ax/ch ax=ah(餘)+al(商)
		int 16h			; ax=0(ah=0) 會暫停

		
		cmp bl, 0		;當加到ff時會溢位變0
		jne next

		mov dl, bx
		call print

		mov ax, 4c00h
		int 21h
;-----------輸出字元fun-----------------
print	proc near
		add dl, 48
		cmp dl, '9'
		jbe show
		add dl, 7
show:	mov ah, 2
		int 21h
		ret
print	endp
;----------------------------------------

;-----------輸出ASCLLfun-----------------
p_ascii	proc near
		mov ah, 2
		int 21h
		ret
p_ascii	endp
;----------------------------------------
code ENDS
	END start
code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h

start:	jmp short begin
hex_n dw ?
begin:	mov bx, 0
next:	mov ah, 0
		int 16h
		cmp al, '='
		je cmpu
		cmp al, '0'
		jb next
		cmp al, '9'
		ja ch_asc
		sub al, 30h
		jmp short add_n
ch_asc: and al, 0dfh		;先轉大寫英文字1011 1111遮罩第2個 因為小寫字母都差32
		cmp al, 'A'
		jb next
		cmp al, 'F'
		ja next
		sub al, 37h
add_n:	mov dl, al			;al搬到dl準備顯示
		push ax				;顯示時會使用到ah所以先將原本的push才不會影響
		call print			;輸出
		pop ax				

		mov cl, 4
		shl bx, cl			;往左位移4bit
		cbw					;al 8位元轉16位元搬入ax
		add bx, ax

		mov dh,bh			;開始檢查是否下次輸入會大於FFFF
		cmp dh, 00fh
		ja cmpu
		jmp next
cmpu:	mov dl, 'h'
		call p_ascii
		mov dl, '='
		call p_ascii
		
		mov cx, 10000
		mov dx, 0
		mov ax, bx
		div cx
		xchg ax, dx			;將餘數放在ax 商放在dx
		mov bx, ax			;餘數存入bx暫存器讓下一個計算
		call print

		mov cx, 1000
		mov dx, 0
		mov ax, bx
		div cx
		xchg ax, dx			;將餘數放在ax 商放在dx
		mov bx, ax			;餘數存入bx暫存器讓下一個計算
		call print
		
		mov cx, 100
		mov dx, 0
		mov ax, bx
		div cx
		xchg ax, dx			;將餘數放在ax 商放在dx
		mov bx, ax			;餘數存入bx暫存器讓下一個計算
		call print

		mov cx, 10
		mov dx, 0
		mov ax, bx
		div cx
		xchg ax, dx			;將餘數放在ax 商放在dx
		mov bx, ax			;餘數存入bx暫存器讓下一個計算
		call print

		mov dx, bx
		call print

		mov ax, 4c00h
		int 21h

;-----------輸出字元fun-----------------
print_8bit	proc near
		mov bl, dl
		shr dl, cl
		call print
		mov dl, bl
		and dl, 0fh
		call print
		ret
print_8bit	endp
;----------------------------------------


;-----------輸出字元fun-----------------
print	proc near
		and dl, 00fh			;遮蔽左邊
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
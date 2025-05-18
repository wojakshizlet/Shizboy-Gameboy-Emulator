package main

import "fmt"

func main() {
	fmt.Println("Hello World!")

}

type registers struct { // declare a struct containing unsigned 8 bit registers

	a uint8 //accumulator
	b uint8 //loop counter
	c uint8 //used in tandem with b, for bc, for unsigned 16 bit registers
	d uint8 //general purpose or temporary data
	e uint8 //used in tandem with d, for de, for memory addresses or I/O
	f uint8 //flags register, very special
	h uint8 //to be paired with l, for hl. high byte of hl, and used for memory addressing
	l uint8 //to be paired with l, for hl. low byte of hl, and used for memory addressing

	//merged registers: optional but a good safety net
	bc uint8 //unsigned 16 bit register
	de uint8 //storing memory addresses or I/O
	hl uint8 //the address register pair. used for referencing memory addresses. (high byte + low byte)
	// some register instructions may also increment or decrement 'hl' after use. like a memory cursor to a loop.
}

func (r *registers) bcGetRegister() uint16 { //
	// returns a bitwise; between register b converted to uint16
	return uint16(r.b)<<8 | uint16(r.c)
}
func (r *registers) bcSetRegister(value uint16) { //
	//b = high byte; if we perform logical & with 0xFF00 and given value, and then right perform bit shift by 8 for big endian, we get high byte
	//c = low byte. if we perform logical & with 0x00FF and given value, we get low byte
	r.b = uint8((value & 0xFF00) >> 8)
	r.c = uint8(value & 0x00FF)
}

func (r *registers) deGetRegister() uint16 { //
	// todo: implement register functionality here
	return uint16(r.d)<<8 | uint16(r.e)
}
func (r *registers) deSetRegister(value uint16) { //
	// todo: implement register functionality here
	r.d = uint8((value & 0xFF00) >> 8)
	r.e = uint8(value & 0x00FF)
}

func (r *registers) hlGetRegister() uint16 { //
	// todo: implement register functionality here
	return uint16(r.h)<<8 | uint16(r.l)
}
func (r *registers) hlSetRegister(value uint16) { //
	// todo: implement register functionality here
	r.h = uint8((value & 0xFF00) >> 8)
	r.l = uint8(value & 0x00FF)
}

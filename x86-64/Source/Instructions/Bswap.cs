﻿#region Copyright and License
/*
 * SharpAssembler
 * Library for .NET that assembles a predetermined list of
 * instructions into machine code.
 * 
 * Copyright (C) 2011 Daniël Pelsmaeker
 * 
 * This file is part of SharpAssembler.
 * 
 * SharpAssembler is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * SharpAssembler is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with SharpAssembler.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.x86.Operands;

namespace SharpAssembler.x86.Instructions
{
	/// <summary>
	/// The BSWAP (Byte Swap) instruction.
	/// </summary>
	public class Bswap : Instruction
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Bswap"/> class.
		/// </summary>
		/// <param name="subject">The register whose bytes will be swapped.</param>
		public Bswap(RegisterOperand subject)
		{
			#region Contract
			Contract.Requires<ArgumentNullException>(subject != null);
			#endregion

			this.subject = subject;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the mnemonic of the instruction.
		/// </summary>
		/// <value>The mnemonic of the instruction.</value>
		public override string Mnemonic
		{
			get { return "bswap"; }
		}

		private RegisterOperand subject;
		/// <summary>
		/// Gets the register whose bytes are swapped.
		/// </summary>
		/// <value>A <see cref="RegisterOperand"/> operand.</value>
		public RegisterOperand Subject
		{
			get
			{
				#region Contract
				Contract.Ensures(Contract.Result<RegisterOperand>() != null);
				#endregion
				return subject;
			}
#if OPERAND_SET
			set
			{
				#region Contract
				Contract.Requires<ArgumentNullException>(value != null);
				#endregion
				subject = value;
			}
#endif
		}
		#endregion

		#region Methods
		/// <summary>
		/// Enumerates an ordered list of operands used by this instruction.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Operand"/> objects.</returns>
		public override IEnumerable<Operand> GetOperands()
		{
			// The order is important here!
			yield return this.subject;
		}
		#endregion

		#region Instruction Variants
		/// <summary>
		/// An array of <see cref="Instruction.InstructionVariant"/> objects
		/// describing the possible variants of this instruction.
		/// </summary>
		private static InstructionVariant[] variants = new[]{
			// BSWAP reg32
			new InstructionVariant(
				new byte[] { 0x0F, 0xC8 },
				new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit,
					OperandEncoding.OpcodeAdd)),
			// BSWAP reg64
			new InstructionVariant(
				new byte[] { 0x0F, 0xC8 },
				new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit,
					OperandEncoding.OpcodeAdd)),
		};

		/// <summary>
		/// Returns an array containing the <see cref="Instruction.InstructionVariant"/>
		/// objects representing all the possible variants of this instruction.
		/// </summary>
		/// <returns>An array of <see cref="Instruction.InstructionVariant"/>
		/// objects.</returns>
		internal override InstructionVariant[] GetVariantList()
		{ return variants; }
		#endregion

		#region Invariant
		/// <summary>
		/// Asserts the invariants of this type.
		/// </summary>
		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(this.subject != null);
		}
		#endregion
	}
}

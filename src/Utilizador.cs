using System;
using Pintura;
using System.Collections.Generic;

namespace Utilizador{
	public class Utilizador {
		private String email;
		private int telefone;
		private String rua;
		private String localidade;
		private String cidade;
		private String codigoPostal;
		private String paisResidencia;
		private String iBAN;
		private String palavraPasse;

		private PinturaDAO pinturas;
		private List<int> codPinturas;
	}
}
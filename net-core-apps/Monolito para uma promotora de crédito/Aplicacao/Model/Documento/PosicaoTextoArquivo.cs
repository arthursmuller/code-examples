using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Model.Documento
{
    public class PosicaoTextoArquivo
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public PosicaoTextoArquivo() { }

        public PosicaoTextoArquivo(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }
    }


}

namespace Backend_Recyclo_dotnet.Models
{
    public static class ID
    {
        public enum Tabela
        {
            empresa,
            usuario,
            ponto,
            denuncia
        }

        public static int newID(Tabela tabela )
        {
            int _id =0;
            Random random = new Random();
            dct8nq053j6k6dContext context = new dct8nq053j6k6dContext();

            while (_id != 0)
            {
                _id = random.Next(999999999);
                switch (tabela)
                {
                    case Tabela.empresa:
                        
                        var verificaEmpresa = context.TbEmpresas.FirstOrDefault(
                            x=>x.CdEmpresa == _id);
                        if (verificaEmpresa is not null)
                            _id =0;
                    break;

                    case Tabela.usuario:

                        var verificaUser = context.TbUsuarios.FirstOrDefault(
                            x=>x.CdUsuario == _id);
                        if (verificaUser is not null)
                            _id =0;
                    break;

                    case Tabela.ponto:
                        var verificaPonto = context.TbPontoColeta.FirstOrDefault(
                            x=>x.CdPontoColeta== _id);
                        if (verificaPonto is not null)
                            _id =0;
                    break;

                    case Tabela.denuncia:
                            var verificaDenuncia = context.TbDenuncia.FirstOrDefault(
                            x=>x.CdDenuncia == _id);
                        if (verificaDenuncia is not null)
                            _id =0;
                    break;                

                    default:
                        _id=0;
                    break;
                }
            }            
            return _id;
        }
    }
}
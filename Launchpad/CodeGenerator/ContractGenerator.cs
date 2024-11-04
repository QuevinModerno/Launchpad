
using Agap2It.Labs.Launchpad.CodeGenerator.Enums;
using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Generators;

namespace Agap2It.Labs.Launchpad.CodeGenerator
{
    public class ContractGenerator : IContractGenerator
    {
        private Erc20Generator Erc20Generator;
        private FeesGenerator FeesGenerator;
        private BurnGenerator BurnGenerator;
        private VoteGenerator VoteGenerator;
        private ReflectionGenerator ReflectionGenerator;
        public ContractGenerator() 
        {
            Erc20Generator = new Erc20Generator();
            FeesGenerator = new FeesGenerator(); 
            BurnGenerator = new BurnGenerator(); 
            VoteGenerator = new VoteGenerator(); 
            ReflectionGenerator = new ReflectionGenerator(); 
        }

        
/*
        public string? GetSmartContract(Erc20Model model, int variant)
        {
            ContractVariations _variant;
            if (model == null)
            {
                throw new ArgumentException("Model");
            }
            else
                if (Enum.IsDefined(typeof(ContractVariations), variant))
                {
                    _variant = (ContractVariations)variant;
                }
                else
                {
                    throw new ArgumentException("Enum");
                }

            return SpecifyCodeGenerator(model, _variant);
        }
*/
        public string GetSmartContract(Erc20Model model, ContractVariations variant)
        {

            switch (variant)
            {
                case ContractVariations.Erc20:      return  Erc20Generator.GetSmartContractCode(model);
                case ContractVariations.Fees:       return  FeesGenerator.GetSmartContractCode(model);
                case ContractVariations.Burn:       return  BurnGenerator.GetSmartContractCode(model);
                case ContractVariations.Vote:       return  VoteGenerator.GetSmartContractCode(model);
                case ContractVariations.Reflection: return  ReflectionGenerator.GetSmartContractCode(model);
            }
            throw new ArgumentException("Generator");
        }
    }
}
/*private static IErc20Transformation? GetTransformation(IErc20Model model)
      {
          switch (model)
          {
              case FeesModel:
                  return new FeesErc20Transformation(model);

              case BurnModel :  
                  return new BurnErc20Transformation(model);

              case VoteModel:
                  return new VoteErc20Transformation(model);

              case StandardModel:
                  return new Erc20Transformation(model);

              default:
                  return null;
          }
      }*/
/* Como e que vai funcionar a tranformacao: Diferencas no code generator
             *a) Tera de ter apenas 1 classe:
                
                    *  Envia o modelo a transformacao.
                    *  retorna o resultado
                    *  Transformacao:
                        *  vai interpretar o modelo
                        *  COm base no modelo, cria o template e a especificacao adequada.
                        *  
                        *  Parece me que a unica solucao e um conjunto infinito de ifs ou switch
                 :. Forma mais simples de implementar se houver uma pequena quantidade de modelos, mas ao longo que a complexidade aumenta, 
                   o codigo torna se mais dificil de entender e manter, criando mais dificuldade em estender a criação de novas variantes de tokens/contratos.
              
             * 
             *b) Tera de ter varias, em cada uma representa os diferentes tokens 
                * (Na mesma)Interpreta qual e o modelo.
                * Cria um objeto transformation especifico
                * Esse transformation ja sabe qual o template  especificacao que vai usar!
                * As verificacoes sao feitas em cada objeto de tasnforamcao porque so lida com um unico modelo!
                * Cada classe transformation cria sempre o mesmo tempalte e especificacao
                * 
            Resposta: A mais apropriada parece me a b), 
            * E porque? qual a justificao?
            * Torna o codigo mais modular e facil de manter. Aumenta a extensabilidade, porque existe uma seperação mais clara de objetos e de funcionalidades.
            * Faz com que as verificações dos modelos sejam feitas seperadamente em cada transformação sem a necesidade de criar uma nova função a cada vez 
            * que existe um novo modelo.
            * Fica 
            * n
             */

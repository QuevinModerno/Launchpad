

using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates
{
    public class VoteErc20TemplateBuilder : Erc20TemplateBuilder, IErc20TemplateBuilder<VoteErc20Specification>
    {
        private VoteErc20Specification _specification;

        public VoteErc20TemplateBuilder()
        {
            _specification = new VoteErc20Specification();
        }
        public string Build(VoteErc20Specification specification)
        {
            return base.Build(specification);
        }

        override public Erc20TemplateBuilder Clear()
        {
            _specification = new VoteErc20Specification();
            return this;
        }

        public override string GenerateProperties()
        {
            return @"struct Proposal {
                        string description;
                        uint256 voteCount;
                        bool executed;
                     }

                     Proposal[] public proposals;
                     mapping(address => uint256) public lastVotedProposal;"
                ;
        }

        public override string GenerateFunctions()
        {
            return @"function submitProposal(string memory description) public {
                        proposals.push(Proposal({
                            description: description,            
                            voteCount: 0,
                            executed: false
                        }));
                    }

                    function vote(uint256 proposalIndex) public {
                        require(balanceOf(msg.sender) > 0, ""Only token holders can vote"");" +
                       "require(proposalIndex < proposals.length, \"Invalid proposal index\");" +
                       "require(!proposals[proposalIndex].executed, \"Proposal already executed\");" +

                        "// Simple check to prevent double voting on the same proposal"+
                        "require(lastVotedProposal[msg.sender] != proposalIndex + 1, \"Already voted on this proposal\");" +
                        "lastVotedProposal[msg.sender] = proposalIndex + 1;        " +
                        "proposals[proposalIndex].voteCount += balanceOf(msg.sender);" +
                   "}" +
                  "// A simplified execution function based on vote count, in a real scenario more checks would be needed" +
                   @"function executeProposal(uint256 proposalIndex) public {
                            require(proposalIndex < proposals.length, ""Invalid proposal index"");
                            Proposal storage proposal = proposals[proposalIndex];
                            require(!proposal.executed, ""Proposal already executed"");
                            require(proposal.voteCount > (totalSupply() / 2), ""Not enough votes"");
                         // Execute the proposal logic...
                            proposal.executed = true;
                    }";

        }
    }
}

using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request
{
    public class RetrieveIyziupFormRequest : BaseRequest
    {
        public string Token { set; get; }

        public override string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("token", Token)
                .GetRequestString();
        }
    }
}
﻿using System;

namespace Milvasoft.Iyzipay.Model.V2.Transaction
{
    public class RefundDetailItem
    {
        public long RefundTxId { get; set; }
        public string RefundConversationId { get; set; }
        public string RefundPrice { get; set; }
        public string CurrencyCode { get; set; }
        public string HostReference { get; set; }
        public string AuthCode { get; set; }
        public int RefundStatus { get; set; }
        public Boolean IsAfterSettlement { get; set; }
        public string CreatedDate { get; set; }
        public string ErrorGroup { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

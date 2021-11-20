using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace aiCorporation.NewImproved
{
    public class SalesAgentFileRecord
    {
        private string m_szSalesAgentName;
        private string m_szSalesAgentEmailAddress;
        private string m_szClientName;
        private string m_szClientIdentifier;
        private string m_szBankName;
        private string m_szAccountNumber;
        private string m_szSortCode;
        private string m_szCurrency;

        public string SalesAgentName { get { return (m_szSalesAgentName); } }
        public string SalesAgentEmailAddress { get { return (m_szSalesAgentEmailAddress); } }
        public string ClientName { get { return (m_szClientName); } }
        public string ClientIdentifier { get { return (m_szClientIdentifier); } }
        public string BankName { get { return (m_szBankName); } }
        public string AccountNumber { get { return (m_szAccountNumber); } }
        public string SortCode { get { return (m_szSortCode); } }
        public string Currency { get { return (m_szCurrency); } }

        public static string CsvHeader()
        {
            StringBuilder sbString;

            sbString = new StringBuilder();

            sbString.Append("SalesAgentName,SalesAgentEmailAddress,ClientName,ClientIdentifier,BankName,AccountNumber,SortCode,Currency\r\n");

            return (sbString.ToString());
        }

        public string ToCsvRecord()
        {
            StringBuilder sbString;

            sbString = new StringBuilder();

            sbString.AppendFormat("\"{0}\"", m_szSalesAgentName);
            sbString.AppendFormat(",\"{0}\"", m_szSalesAgentEmailAddress);
            sbString.AppendFormat(",\"{0}\"", m_szClientName);
            sbString.AppendFormat(",\"{0}\"", m_szClientIdentifier);
            sbString.AppendFormat(",\"{0}\"", m_szBankName);
            sbString.AppendFormat(",\"{0}\"", m_szAccountNumber);
            sbString.AppendFormat(",\"{0}\"", m_szSortCode);
            sbString.AppendFormat(",\"{0}\"", m_szCurrency);

            sbString.Append("\r\n");

            return (sbString.ToString());
        }

        public SalesAgentFileRecord(string szSalesAgentName,
                                    string szSalesAgentEmailAddress,
                                    string szClientName,
                                    string szClientIdentifier,
                                    string szBankName,
                                    string szAccountNumber,
                                    string szSortCode,
                                    string szCurrency)
        {
            m_szSalesAgentName = szSalesAgentName;
            m_szSalesAgentEmailAddress = szSalesAgentEmailAddress;
            m_szClientName = szClientName;
            m_szClientIdentifier = szClientIdentifier;
            m_szBankName = szBankName;
            m_szAccountNumber = szAccountNumber;
            m_szSortCode = szSortCode;
            m_szCurrency = szCurrency;
        }
    }
    public class SalesAgentFileRecordList
    {
        private List<SalesAgentFileRecord> m_lsafrSalesAgentFileRecordList;

        public int Count { get { return (m_lsafrSalesAgentFileRecordList.Count); } }

        public SalesAgentFileRecord this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lsafrSalesAgentFileRecordList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lsafrSalesAgentFileRecordList[nIndex]);
            }
        }

        public SalesAgentFileRecord this[string szSalesAgentEmailAddress]
        {
            get
            {
                int nCount = 0;
                bool boFound = false;
                SalesAgentFileRecord safrSalesAgentFileRecord = null;

                while (!boFound &&
                       nCount < m_lsafrSalesAgentFileRecordList.Count)
                {
                    if (m_lsafrSalesAgentFileRecordList[nCount].SalesAgentEmailAddress == szSalesAgentEmailAddress)
                    {
                        boFound = true;
                        safrSalesAgentFileRecord = m_lsafrSalesAgentFileRecordList[nCount];
                    }
                    nCount++;
                }
                return (safrSalesAgentFileRecord);
            }
        }

        public string ToCsvString()
        {
            StringBuilder sbCsvString;
            int nCount;

            sbCsvString = new StringBuilder();

            sbCsvString.Append(SalesAgentFileRecord.CsvHeader());

            for (nCount = 0; nCount < m_lsafrSalesAgentFileRecordList.Count; nCount++)
            {
                sbCsvString.AppendFormat("{0}", m_lsafrSalesAgentFileRecordList[nCount].ToCsvRecord());
            }

            return (sbCsvString.ToString());
        }

        public static SalesAgentFileRecordList FromCsvStream(Stream sStream)
        {
            StreamReader srReader;
            CsvReader crReader;
            SalesAgentFileRecord safrSalesAgentFileRecord;
            List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList;
            SalesAgentFileRecordList safrlSalesAgentFileRecordList;
            string szSalesAgentName;
            string szSalesAgentEmailAddress;
            string szClientName;
            string szClientIdentifier;
            string szBankName;
            string szAccountNumber;
            string szSortCode;
            string szCurrency;
            int nCount;

            lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            if (sStream != null)
            {
                nCount = 0;

                srReader = new StreamReader(sStream);
                crReader = new CsvReader(srReader);

                while (crReader.Read())
                {
                    // don't read in the first row as it's the header data
                    if (nCount > 0)
                    {
                        szSalesAgentName = crReader.GetField<string>(0);
                        szSalesAgentEmailAddress = crReader.GetField<string>(1);
                        szClientName = crReader.GetField<string>(2);
                        szClientIdentifier = crReader.GetField<string>(3);
                        szBankName = crReader.GetField<string>(4);
                        szAccountNumber = crReader.GetField<string>(5);
                        szSortCode = crReader.GetField<string>(6);
                        szCurrency = crReader.GetField<string>(7);

                        safrSalesAgentFileRecord = new SalesAgentFileRecord(szSalesAgentName, szSalesAgentEmailAddress, szClientName, szClientIdentifier, szBankName, szAccountNumber, szSortCode, szCurrency);
                        lsafrSalesAgentFileRecordList.Add(safrSalesAgentFileRecord);
                    }
                    nCount++;
                }
            }
            safrlSalesAgentFileRecordList = new SalesAgentFileRecordList(lsafrSalesAgentFileRecordList);

            return (safrlSalesAgentFileRecordList);
        }

        /************************************************************/
        /* THIS IS THE FUNCTION THAT WE WOULD LIKE YOU TO IMPLEMENT */
        /************************************************************/
        public SalesAgentList ToSalesAgentList()
        {
            SalesAgentList salReturnSalesAgentList;
            SalesAgentListBuilder salSalesAgentList;
            SalesAgentBuilder saCurrentSalesAgent;
            ClientBuilder cClient;
            BankAccountBuilder baBankAccount;
            int nCount;

            salSalesAgentList = new SalesAgentListBuilder();

            for (nCount = 0; nCount < m_lsafrSalesAgentFileRecordList.Count; nCount++)
            {
                if (salSalesAgentList.ContainsAgent(m_lsafrSalesAgentFileRecordList[nCount].SalesAgentEmailAddress))
                {
                    saCurrentSalesAgent = salSalesAgentList[m_lsafrSalesAgentFileRecordList[nCount].SalesAgentEmailAddress];
                    
                }
                else
                {
                    saCurrentSalesAgent = new SalesAgentBuilder();
                    saCurrentSalesAgent.SalesAgentEmailAddress = m_lsafrSalesAgentFileRecordList[nCount].SalesAgentEmailAddress;
                    saCurrentSalesAgent.SalesAgentName = m_lsafrSalesAgentFileRecordList[nCount].SalesAgentName;
                    salSalesAgentList.Add(saCurrentSalesAgent);
                }

                if (saCurrentSalesAgent.ClientList.ContainsClient(m_lsafrSalesAgentFileRecordList[nCount].ClientIdentifier) )
                {
                    cClient = saCurrentSalesAgent.ClientList[m_lsafrSalesAgentFileRecordList[nCount].ClientIdentifier];
                }
                else
                {
                    cClient = new ClientBuilder();
                    cClient.ClientIdentifier = m_lsafrSalesAgentFileRecordList[nCount].ClientIdentifier;
                    cClient.ClientName = m_lsafrSalesAgentFileRecordList[nCount].ClientName;
                    saCurrentSalesAgent.ClientList.Add(cClient);
                }

                baBankAccount = cClient.BankAccountList.GetBankAccount(m_lsafrSalesAgentFileRecordList[nCount].BankName, m_lsafrSalesAgentFileRecordList[nCount].AccountNumber, m_lsafrSalesAgentFileRecordList[nCount].SortCode);

                if (baBankAccount == null)
                {
                    baBankAccount = new BankAccountBuilder();
                    baBankAccount.BankName = m_lsafrSalesAgentFileRecordList[nCount].BankName;
                    baBankAccount.AccountNumber = m_lsafrSalesAgentFileRecordList[nCount].AccountNumber;
                    baBankAccount.SortCode = m_lsafrSalesAgentFileRecordList[nCount].SortCode;
                    cClient.BankAccountList.Add(baBankAccount);
                }

                baBankAccount.Currency = m_lsafrSalesAgentFileRecordList[nCount].Currency;
            }

            salReturnSalesAgentList = new SalesAgentList(salSalesAgentList.GetListOfSalesAgentObjects());

            return (salReturnSalesAgentList);
        }

        /************************************************************/
        /* THIS IS THE FUNCTION THAT WE WOULD LIKE YOU TO IMPLEMENT */
        /************************************************************/
        public SalesAgentList ToSalesAgentListRandom()
        {
            SalesAgentListBuilder salSalesAgentList = new SalesAgentListBuilder();

            var salesAgents = from salesAgent in m_lsafrSalesAgentFileRecordList
                              select new { salesAgent.SalesAgentName, salesAgent.SalesAgentEmailAddress };
            var agents = salesAgents.Distinct();

            var salesAgentClients = from salesAgentClient in m_lsafrSalesAgentFileRecordList
                                    select new { salesAgentClient.SalesAgentName, salesAgentClient.SalesAgentEmailAddress, salesAgentClient.ClientIdentifier, salesAgentClient.ClientName };

            //var clientList = from client in m_lsafrSalesAgentFileRecordList
            //                 select new { client.ClientIdentifier, client.ClientName };
            //var clients = clientList.Distinct();

            //var bankAccountList = from bankAccount in m_lsafrSalesAgentFileRecordList
            //                      select new { bankAccount.ClientIdentifier, bankAccount.AccountNumber, bankAccount.SortCode, bankAccount.Currency };
            //var result = clientList.Distinct();


            foreach (var agent in agents)
            {
                SalesAgentBuilder currentSalesAgent = new SalesAgentBuilder() { SalesAgentEmailAddress = agent.SalesAgentEmailAddress };
                currentSalesAgent.SalesAgentEmailAddress = agent.SalesAgentEmailAddress;
                currentSalesAgent.SalesAgentName = agent.SalesAgentName;

                var agentClient = from client in salesAgentClients
                                  where currentSalesAgent.SalesAgentName == client.SalesAgentName
                                  select new { client.ClientIdentifier, client.ClientName };

                var agentClientAccounts = from client in m_lsafrSalesAgentFileRecordList
                                          where currentSalesAgent.SalesAgentName == client.SalesAgentName
                                          select new { client.ClientIdentifier, client.ClientName, client.BankName, client.AccountNumber, client.SortCode, client.Currency };

                foreach (var client in agentClient)
                {
                    ClientBuilder clientBuilder = new ClientBuilder();
                    clientBuilder.ClientIdentifier = client.ClientIdentifier;
                    clientBuilder.ClientName = client.ClientName;

                    var accounts = from account in agentClientAccounts
                                   where account.ClientIdentifier == client.ClientIdentifier
                                   select new { account.BankName, account.AccountNumber, account.SortCode, account.Currency };

                    foreach (var account in accounts)
                    {
                        BankAccountBuilder bankAccountBuilder = new BankAccountBuilder() { BankName = account.BankName, AccountNumber = account.AccountNumber, Currency = account.Currency, SortCode = account.SortCode };
                        clientBuilder.BankAccountList.Add(bankAccountBuilder);
                    }

                    currentSalesAgent.ClientList.Add(clientBuilder);
                }

                salSalesAgentList.Add(currentSalesAgent);
            }

            return new SalesAgentList(salSalesAgentList.GetListOfSalesAgentObjects());
        }

        public SalesAgentFileRecordList(List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList)
        {
            int nCount = 0;

            m_lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            if (lsafrSalesAgentFileRecordList != null)
            {
                for (nCount = 0; nCount < lsafrSalesAgentFileRecordList.Count; nCount++)
                {
                    m_lsafrSalesAgentFileRecordList.Add(lsafrSalesAgentFileRecordList[nCount]);
                }
            }
        }

        public List<SalesAgentFileRecord> GetListOfSalesAgentFileRecordObjects()
        {
            List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList = null;
            int nCount = 0;

            lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            for (nCount = 0; nCount < m_lsafrSalesAgentFileRecordList.Count; nCount++)
            {
                lsafrSalesAgentFileRecordList.Add(m_lsafrSalesAgentFileRecordList[nCount]);
            }

            return (lsafrSalesAgentFileRecordList);
        }
    }
}
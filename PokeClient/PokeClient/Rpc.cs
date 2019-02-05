using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;

public class Rpc
{


    public async Task<string> CallApi(string queue, List<string> args)
    {
        MemoryStream ms = new MemoryStream();

        DataContractJsonSerializer ser = new DataContractJsonSerializer((args.GetType()));
        ser.WriteObject(ms, args);
        byte[] json = ms.ToArray();
        ms.Close();
        String msg = Encoding.UTF8.GetString(json, 0, json.Length);

        return await InvokeAsync(msg, queue);
    }

    private async Task<string> InvokeAsync(string n, string queue)
    {
        var rpcClient = new RpcClient();
        var response = await rpcClient.CallAsync(n.ToString(), queue);

        rpcClient.Close();

        return response;

    }

}
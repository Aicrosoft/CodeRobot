//import System;
import CS.Logging;
import CS.CodeRobot.Generators;
class JsProgram {

    var log:ILog = LogManager.GetLogger();

    function main(app) {
        //log.Error("hellow");
        for (var model in app.Models) {
            log.Info("开始生成["+ model.Name +"]的Assembly.cs");
            //log.Debug(model);
			Generator.CreateAssembly(model);
			
			log.Info("开始生成["+ model.Name +"]的相关config文件");
			Generator.CreateConfigs(model);
			
			log.Info("开始生成["+ model.Name +"]的项目文件 "+ model.AssemblyName +".csproj");
			Generator.CreateProjetc(model);

        }

    }

}

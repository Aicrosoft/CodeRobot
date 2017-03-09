//import System;
import CS.Logging;
import CS.CodeRobot.Generators;
class JsProgram {

    var log:ILog = LogManager.GetLogger();

    function main(app,model) {
		
        log.Info("初始化["+ model.Name +"]的项目文件集合CodeFiles");
		Generator.InitCodeFiles();
			
			
        for (var dbSet in app.Project.DbConns) {
			
			log.Error(dbSet.Tables.Count);
           

        }

    }

}

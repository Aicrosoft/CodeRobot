//import System;
import CS.Logging;
import CS.CodeRobot.Generators;
class JsProgram {

    var log:ILog = LogManager.GetLogger();

    function main(app,model) {
		
		
        log.Info("初始化["+ model.Name +"]的项目文件集合CodeFiles");
		Generator.InitCodeFiles();
			
			
        for (var dbSet in app.Project.DbConns) {
			log.Debug("预计有："+ dbSet.Tables.Count + "个表要进行处理")
			
			
			//开始处理表
			for(var tb in dbSet.Tables){
				//log.Error(tb.Name);
				
				Generator.RenderTable("Item.tpl",model,dbSet,tb);
				
			}
           
        }
		
		log.Info("开始更新["+ model.Name +"]的项目的工程文件");
		Generator.UpdateProject(model);

    }

}

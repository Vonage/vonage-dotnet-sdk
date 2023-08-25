var fs   = require('fs');
var path = require('path');
var exec = require('child_process').exec;
var hiddenHooksFolderPath = path.join(__dirname, '../.git/hooks');
var hooksFolderPath       = path.join(__dirname, 'git_hooks');
var hooks                 = fs.readdirSync(hooksFolderPath);
function copyFile (source, target, cb) {
  var cbCalled = false;
  var rd       = fs.createReadStream(source);
  var wr       = fs.createWriteStream(target);
function done(err) {
    if (!cbCalled) {
      cb(err);
      cbCalled = true;
    }
  }
rd.on("error", done);
  wr.on("error", done);
  wr.on("close", done);
  rd.pipe(wr);
}
hooks.forEach(function (hook) {
  var hookSource = path.join(hooksFolderPath, hook);
  var hookTarget = path.join(hiddenHooksFolderPath, hook);
copyFile(hookSource, hookTarget, function (err) {
    if (!err) {
      console.log(hook + ' added to your .git/hooks folder')
      exec(
        'chmod +x ' + hookTarget,
        function (error) {
          if (!error) {
            console.log(hookTarget + ' made executable');
          }
        }
      );
    }
  })
});
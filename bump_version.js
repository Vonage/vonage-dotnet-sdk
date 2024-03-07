const os = require("os"),
    fileSystem = require("fs"),
    xmlBuilder = require("xml2js"),
    parseString = require("xml2js").parseString,
    spawnSync = require("child_process").spawnSync;

class VersionUpgrade {
    projectFile = "Vonage/Vonage.csproj";

    _runRelease() {
        console.log("Reading project file...");
        fileSystem.readFile(this.projectFile, "utf-8", (err, data) => {
            if (err) {
                console.log(err);
                return;
            }

            console.log("Parsing project data...");
            parseString(data, (err, result) => {
                if (err) {
                    console.log(err);
                    return;
                }

                console.log("Parsing successful");
                this.UpdateProjectData(result);
                console.log("Project data updated");
                fileSystem.writeFile(this.projectFile, new xmlBuilder.Builder().buildObject(result), (err, res) => {
                    if (err) {
                        console.log(err);
                        return;
                    }

                    console.log("Successfully wrote project data");
                    this._executeCommand(`git add ${this.projectFile}`);
                    this._executeCommandWithArgs(`git`, ["commit", "-m", `'docs: bump version to ${this.tag}'`]);
                    this._executeCommand(`git tag -f ${this.tag}`);
                    this._executeCommand(`git cliff -o CHANGELOG.md`);
                    this._executeCommandWithArgs(`git`, ["commit", "-m", `'docs: generate changelog for ${this.tag}'`]);
                    this._executeCommand(`git push`);
                    this._executeCommand(`git push origin ${this.tag} --force`);
                });
            });
        })
    }

    UpdateProjectData(result) {
        result.Project.PropertyGroup[0].Version[0] = this.version;
        result.Project.PropertyGroup[0].PackageReleaseNotes[0] = `https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/` + this.tag
    }

    _executeCommand(cmd, options) {
        console.log(`executing: [${cmd}]`)
        let ops = {
            cwd: process.cwd(),
            env: process.env,
            stdio: 'pipe',
            encoding: 'utf-8'
        };
        const INPUT = cmd.split(" "), TOOL = INPUT[0], ARGS = INPUT.slice(1)
        console.log(String(spawnSync(TOOL, ARGS, ops).output));
    }

    _executeCommandWithArgs(cmd, args) {
        console.log(`executing: [${cmd}]`)
        let ops = {
            cwd: process.cwd(),
            env: process.env,
            stdio: 'pipe',
            encoding: 'utf-8'
        };
        console.log(String(spawnSync(cmd, args, ops).output));
    }

    upgrade(argv) {
        console.log("Upgrading version...");
        if (argv.length <= 2) {
            console.log("The 'version' argument is missing.");
            return;
        }

        this.version = argv[2];
        this.tag = "v" + this.version;
        console.log("Bumping version to " + this.version);
        console.log("Bumping tag to " + this.tag);
        this._runRelease();
    }
}

new VersionUpgrade().upgrade(process.argv);
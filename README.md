# Admiral
Windows automation tool: powerful as Powershell, easy as Next-Next-Finish.

[![Build status](https://ci.appveyor.com/api/projects/status/suqwwsqyy7c3k6c4?svg=true)](https://ci.appveyor.com/project/AdmiralTool/admiral)
## What is Admiral
> Why nobody tried to build Ansible module which sets up an Active Directory?

This was our first question to ask ourselves but not the last.
We want to make Windows automation easy and we want to do it powerful. With admiral you can use whole Powershell and do it, writing a simple yaml file with servers to manage and instructions to run.

Define your machines like:
```
---
- name: prod
  hosts:
    - 192.168.1.1
    - prod1.acme.com
    - prod2.acme.com
- name: dns
  hosts:
    - ns1.acme.com
    - ns2.acme.com
    - ns3.acme.com
```

And a config yaml:
```
---
ships: prod
username: poweruser
password: superpassword
tasks:
  - name: stop the spooler
    module: service.listsrv
    args: name=spooler action=stop
```

More documentation to come and more features to support, so stay tuned!
Your feedback is always appreciated!

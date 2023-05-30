#!/bin/sh

project_using_ef=Pbfl.API

echo "ef-migrations.sh [add/list/remove/rollback] [migration-name]"

action=$1
name=$2

script_dir=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)
echo $script_dir
add_or_remove=false

if [ "$action" = "add" ]; then
  if [ "$name" != "" ]; then
    add_or_remove=true
  else
    echo "To add a migration you must provide a name."
    exit
  fi
elif [ "$action" = "remove" ]; then
  add_or_remove=true
  if [ "$name" != "" ]; then
    echo "You can only remove the last migration, no name will be used."
    name=
  fi
fi 

start_dir=$(pwd)
website_dir=$script_dir/../$project_using_ef
cd "$website_dir" || return
pwd

if [ $add_or_remove = true ]; then
  echo "dotnet ef migrations "$action" $name --project ../Migrations/SqliteMigrations -- --DatabaseProvider Sqlite"
#  dotnet ef migrations "$action" $name --project ../Migrations/PostgresMigrations -- --DatabaseProvider Postgres
  dotnet ef migrations "$action" $name --project ../Migrations/SqliteMigrations -- --DatabaseProvider Sqlite
else
  if [ "$action" = "rollback" ]; then
    if [ "$name" = "" ]; then
      echo To rollback to a previous migration, you need to specify the name.
    else
      echo dotnet ef database update $name
      dotnet ef database update $name
    fi
  elif [ "$action" = "update" ]; then
    dotnet ef database update $name
  elif [ "$action" != "" ]; then
    dotnet ef migrations "$@"
  else
    dotnet ef migrations list
  fi
fi

cd "$start_dir" || return

#read -p "Press enter to close"

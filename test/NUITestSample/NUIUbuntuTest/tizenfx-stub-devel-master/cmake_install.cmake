# Install script for directory: /home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master

# Set the install prefix
if(NOT DEFINED CMAKE_INSTALL_PREFIX)
  set(CMAKE_INSTALL_PREFIX "/home/huiyu/dali-env/opt")
endif()
string(REGEX REPLACE "/$" "" CMAKE_INSTALL_PREFIX "${CMAKE_INSTALL_PREFIX}")

# Set the install configuration name.
if(NOT DEFINED CMAKE_INSTALL_CONFIG_NAME)
  if(BUILD_TYPE)
    string(REGEX REPLACE "^[^A-Za-z0-9_]+" ""
           CMAKE_INSTALL_CONFIG_NAME "${BUILD_TYPE}")
  else()
    set(CMAKE_INSTALL_CONFIG_NAME "Debug")
  endif()
  message(STATUS "Install configuration: \"${CMAKE_INSTALL_CONFIG_NAME}\"")
endif()

# Set the component getting installed.
if(NOT CMAKE_INSTALL_COMPONENT)
  if(COMPONENT)
    message(STATUS "Install component: \"${COMPONENT}\"")
    set(CMAKE_INSTALL_COMPONENT "${COMPONENT}")
  else()
    set(CMAKE_INSTALL_COMPONENT)
  endif()
endif()

# Install shared libraries without execute permission?
if(NOT DEFINED CMAKE_INSTALL_SO_NO_EXE)
  set(CMAKE_INSTALL_SO_NO_EXE "1")
endif()

if(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "RuntimeLibraries")
  list(APPEND CMAKE_ABSOLUTE_DESTINATION_FILES
   "/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-common.so.1;/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-common.so.0;/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-common.so")
  if(CMAKE_WARN_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(WARNING "ABSOLUTE path INSTALL DESTINATION : ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
  if(CMAKE_ERROR_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(FATAL_ERROR "ABSOLUTE path INSTALL DESTINATION forbidden (by caller): ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
file(INSTALL DESTINATION "/home/huiyu/dali-env/opt/lib" TYPE SHARED_LIBRARY FILES
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libcapi-appfw-app-common.so.1"
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libcapi-appfw-app-common.so.0"
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libcapi-appfw-app-common.so"
    )
  foreach(file
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-common.so.1"
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-common.so.0"
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-common.so"
      )
    if(EXISTS "${file}" AND
       NOT IS_SYMLINK "${file}")
      if(CMAKE_INSTALL_DO_STRIP)
        execute_process(COMMAND "/usr/bin/strip" "${file}")
      endif()
    endif()
  endforeach()
endif()

if(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "RuntimeLibraries")
  list(APPEND CMAKE_ABSOLUTE_DESTINATION_FILES
   "/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-manager.so.1;/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-manager.so.0;/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-manager.so")
  if(CMAKE_WARN_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(WARNING "ABSOLUTE path INSTALL DESTINATION : ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
  if(CMAKE_ERROR_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(FATAL_ERROR "ABSOLUTE path INSTALL DESTINATION forbidden (by caller): ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
file(INSTALL DESTINATION "/home/huiyu/dali-env/opt/lib" TYPE SHARED_LIBRARY FILES
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libcapi-appfw-app-manager.so.1"
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libcapi-appfw-app-manager.so.0"
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libcapi-appfw-app-manager.so"
    )
  foreach(file
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-manager.so.1"
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-manager.so.0"
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libcapi-appfw-app-manager.so"
      )
    if(EXISTS "${file}" AND
       NOT IS_SYMLINK "${file}")
      if(CMAKE_INSTALL_DO_STRIP)
        execute_process(COMMAND "/usr/bin/strip" "${file}")
      endif()
    endif()
  endforeach()
endif()

if(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "RuntimeLibraries")
  list(APPEND CMAKE_ABSOLUTE_DESTINATION_FILES
   "/home/huiyu/dali-env/opt/lib/libdlog.so.1;/home/huiyu/dali-env/opt/lib/libdlog.so.0;/home/huiyu/dali-env/opt/lib/libdlog.so")
  if(CMAKE_WARN_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(WARNING "ABSOLUTE path INSTALL DESTINATION : ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
  if(CMAKE_ERROR_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(FATAL_ERROR "ABSOLUTE path INSTALL DESTINATION forbidden (by caller): ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
file(INSTALL DESTINATION "/home/huiyu/dali-env/opt/lib" TYPE SHARED_LIBRARY FILES
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libdlog.so.1"
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libdlog.so.0"
    "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/libdlog.so"
    )
  foreach(file
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libdlog.so.1"
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libdlog.so.0"
      "$ENV{DESTDIR}/home/huiyu/dali-env/opt/lib/libdlog.so"
      )
    if(EXISTS "${file}" AND
       NOT IS_SYMLINK "${file}")
      if(CMAKE_INSTALL_DO_STRIP)
        execute_process(COMMAND "/usr/bin/strip" "${file}")
      endif()
    endif()
  endforeach()
endif()

if(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/include/app-common" TYPE FILE FILES "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/include/app_common.h")
endif()

if(CMAKE_INSTALL_COMPONENT)
  set(CMAKE_INSTALL_MANIFEST "install_manifest_${CMAKE_INSTALL_COMPONENT}.txt")
else()
  set(CMAKE_INSTALL_MANIFEST "install_manifest.txt")
endif()

string(REPLACE ";" "\n" CMAKE_INSTALL_MANIFEST_CONTENT
       "${CMAKE_INSTALL_MANIFEST_FILES}")
file(WRITE "/home/huiyu/Shared/workspace/tizenfx/github/TizenFX/test/NUITestSample/NUIUbuntuTest/tizenfx-stub-devel-master/${CMAKE_INSTALL_MANIFEST}"
     "${CMAKE_INSTALL_MANIFEST_CONTENT}")
